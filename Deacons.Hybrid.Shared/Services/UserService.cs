using Azure.Storage.Blobs;
using Dapper;
using Deacons.Hybrid.Shared.Interface;
using Deacons.Hybrid.Shared.Models;
using Deacons.Hybrid.Shared.Services.Interface;
using Deacons.Hybrid.Shared.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Deacons.Hybrid.Shared.Services
{
    public class UsersService : IUsersService
    {
        private readonly IDapperContrib _dapperContrib;
        private string _connectionString { get; set; }
        BlobServiceClient _blobClient;
        BlobContainerClient _containerClient;

        public UsersService(BlobServiceClient blobServiceClient, IDapperContrib dapperContrib, IConfiguration configuration)
        {
            _blobClient = blobServiceClient;
            _containerClient = _blobClient.GetBlobContainerClient("imagescontainer/avatars");

            _dapperContrib = dapperContrib;
            _connectionString = "Data Source=67.211.213.157,1433;Database=Deacons;Integrated Security=false;TrustServerCertificate=true;Persist Security Info=True;User ID=sa;Password=Logvc123!";

            //var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //if (env == EnvironmentName.Development)
            //{
            //    _connectionString = configuration.GetConnectionString("DevStaffConnString");
            //}
            //else
            //{
            //    _connectionString = configuration.GetConnectionString("StaffConnString");
            //}
        }

        public async Task Create(User user)
        {
            try
            {
                await _dapperContrib.Insert(user);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<object?> GetAllUsers()
        {
            return await _dapperContrib.GetAll<User>();
        }

        public async Task<List<User>> GetAllCheckIns()
        {

            var results = new List<User>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var lookup = new Dictionary<Guid, User>();
                var userModel = connection.Query<User, AttendanceModel, PostLocationModel, User>(@"
                                    SELECT u.*, a.*, pl.PostLocationId as PostId, pl.PostLocationName
                                    FROM Users u
                                    JOIN Attendance a ON u.UserId = a.UserId
                                    Join PostLocations pl on a.PostLocationId = pl.PostLocationId
                                    Order By a.CheckInTime Desc
                                   "
                      , (u, a, pl) => {
                          User userModel;
                          if (!lookup.TryGetValue(u.UserId, out userModel))
                          {
                              lookup.Add(u.UserId, userModel = u);
                          }
                          if (userModel.UserCheckIns == null)
                          {
                              userModel.UserCheckIns = new List<AttendanceModel>();
                          }
                          a.PostName = pl.PostLocationName;
                         // a.PostName = p.PostName;
                          userModel.UserCheckIns.Add(a);
                          return userModel;
                      },
                    splitOn: "AttendanceId, PostId"
                    ).AsQueryable();

                results = lookup.Values.OrderBy(o => o?.UserCheckIns?.OrderBy(u => u.CheckInTime)).ToList();
            }

            return results;
        }

        public async Task<UserCheckInModel> UpdateAttendanceById(UserCheckInModel model)
        {

            var start = Convert.ToDateTime(model.CheckInTime).ToUniversalTime();
            var end = Convert.ToDateTime(model.CheckOutTime).ToUniversalTime();
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(start, tzi);
            DateTime localEndTime = TimeZoneInfo.ConvertTimeFromUtc(end, tzi);
            model.CheckInTime = localTime;
            model.CheckOutTime = localEndTime;
            var sql = $@"Update Attendance
                             Set CheckInTime = @CheckInTime,
                                CheckOutTime = @CheckOutTime,
                                PostLocationId = @PostLocationId,
                                PostId = @PostId,
                                PostName = @PostName
                                Where AttendanceId = @AttendanceId;";
            
            var paramDetails = new DynamicParameters();

            paramDetails.Add("@CheckInTime", model.CheckInTime);
            paramDetails.Add("@CheckOutTime", model.CheckOutTime);
            paramDetails.Add("@PostLocationId", model.PostLocationId);
            paramDetails.Add("@PostName", model.PostName);
            paramDetails.Add("@PostId", model.PostId);
            paramDetails.Add("@AttendanceId", model.AttendanceId);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteScalarAsync<object>(sql, paramDetails);
            }
            return model;
        }

        public async Task<User> GetAllCheckInsByUserId(Guid id)
        {

            var results = new User();
            var aql = $@"SELECT s.*, a.*, p.PostLocationId as PostId, p.PostLocationName
                        FROM Users s
                        JOIN Attendance a ON a.UserId = s.UserId
                        Join PostLocations p on a.PostLocationId = p.PostLocationId
                        Where a.UserId = '{id}'
                        Order By a.CheckInTime DESC";
            using (var connection = new SqlConnection(_connectionString))
            {
                var lookup = new Dictionary<Guid, User>();
                var userModel = connection.Query<User, AttendanceModel, PostLocationModel, User>(aql,
                                    (user, attendance, post) =>
                                    {
                                        User userModel;
                                        if (!lookup.TryGetValue(user.UserId, out userModel))
                                        {
                                            lookup.Add(user.UserId, userModel = user);
                                        }
                                        if (userModel.UserCheckIns == null)
                                        {
                                            userModel.UserCheckIns = new List<AttendanceModel>();
                                        }
                                        attendance.PostLocation = post.PostLocationName;
                                        userModel.UserCheckIns.Add(attendance);
                                        return userModel;
                                    },
               splitOn: "AttendanceId, PostId");

                results = lookup.Values.FirstOrDefault();
            }

            return results;
        }


        public async Task<List<ChurchEventsModel>> GetAllEventNames()
        {
            return (List<ChurchEventsModel>)await _dapperContrib.GetAll<ChurchEventsModel>();
        }

            public async Task<IEnumerable<TeamUserModel>> GetAll()
        {
            var results = new List<TeamUserModel>();
            var sql =
                $@"Select u.FirstName, u.LastName, u.UserId,  u.AvatarUrl, u.PhoneNumber, u.Email, dt.DeaconTitle, dt.DeaconPosition, t.TeamName from Users u
	                    join Teams t on t.TeamId = u.TeamId
                    	join DeaconTitle dt on dt.TitleId = u.TitleId";
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var res = await connection.QueryAsync<TeamUserModel>(sql);
                foreach (var result in res.ToList())
                {
                    var name = result.FirstName + " " + result.LastName;
                    result.Initials = UtilityService.GetInitials(name, "");
                    results.Add(result);
                }
            }

            return results;
        }
        public async Task<object?> UpdateUserProfile(User newUser)
        {
            newUser.BiometricsEnabled = newUser.BiometricsEnabled == null ? newUser.BiometricsEnabled = false : newUser.BiometricsEnabled;
            newUser.NotificationsEnabled = newUser.NotificationsEnabled == null ? newUser.NotificationsEnabled = false : newUser.NotificationsEnabled;
            newUser.LocationEnabled = newUser.LocationEnabled == null ? newUser.LocationEnabled = false : newUser.LocationEnabled;

            var retVal = new Object();
            var sql = $@"Update Users
                             Set FirstName = @FirstName,
                                LastName = @LastName,
                                PhoneNumber = @PhoneNumber,
                                Email = @Email,
                                Address = @Address,
                                City = @City,
                                State = @State,
                                Zip = @Zip,
                                AboutMe = @AboutMe,
                                ModifiedDate = '{DateTime.Now}',
                                BiometricsEnabled = @BiometricsEnabled,
                                NotificationsEnabled = @NotificationsEnabled,
                                LocationEnabled = @LocationEnabled
                                Where UserId = @UserId;";

            if (newUser.BiometricsEnabled != null)
            {

            }
            var paramDetails = new DynamicParameters();

            paramDetails.Add("@FirstName", newUser.FirstName);
            paramDetails.Add("@LastName", newUser.LastName);
            paramDetails.Add("@PhoneNumber", newUser.PhoneNumber);
            paramDetails.Add("@Email", newUser.Email);
            paramDetails.Add("@State", newUser.State);
            paramDetails.Add("@Zip", newUser.Zip);
            paramDetails.Add("@City", newUser.City);
            paramDetails.Add("@Address", newUser.Address);
            paramDetails.Add("@AboutMe", newUser.AboutMe);
            paramDetails.Add("@NotificationsEnabled", newUser.NotificationsEnabled);
            paramDetails.Add("@BiometricsEnabled", newUser.BiometricsEnabled);
            paramDetails.Add("@LocationEnabled", newUser.LocationEnabled);
            paramDetails.Add("@UserId", newUser.UserId);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                retVal = await connection.ExecuteScalarAsync<object>(sql, paramDetails);
            }
            return retVal;
        }

        public async Task<RegistrationCodes> GetRegistrationCodeDetails(Guid id)
        {
            return await _dapperContrib.Get<RegistrationCodes>(id);
        }

        public async Task UpdateRegistrationCodeDetails(RegistrationCodes registration, User user)
        {
            var sql = $@"Update DeaconCodes
                             Set UserId = @UserId,
                             LastUsedDateTime '{DateTime.Now}'
                             Where CodeId = '@CodeId';

                         Update User
                             Set DeaconCodeId = @CodeId,
                             LastUsedDateTime '{DateTime.Now}'
                             Where UserId = @UserId;";
            var paramDetails = new DynamicParameters();

            paramDetails.Add("@CodeId", registration.CodeId);
            paramDetails.Add("@UserId", user.UserId);
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var retVal = await connection.ExecuteScalarAsync<Guid>(sql, paramDetails);
            }
        }

        private string RandomNumber()
        {
            var random = new Random();
            int randNum = random.Next(19999999);
            var sixDigitNumber = randNum.ToString("D6");
            return sixDigitNumber;
        }

        public async Task<object?> UploadImages(IFormFile file)
        {

            var azureResponse = string.Empty;
            string fileName = $@"avatars/{file.FileName}";
            try
            {
                BlobClient blobClient = _containerClient.GetBlobClient(fileName);

                blobClient.DeleteIfExists();

                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    var client = await _containerClient.UploadBlobAsync(fileName, memoryStream, default);


                    blobClient = _containerClient.GetBlobClient(fileName);
                    azureResponse = blobClient.Uri.AbsoluteUri;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return azureResponse;
        }

        public async Task<bool> DeleteProfileImage(string avatarUrl)
        {

            var azureResponse = false;

            BlobClient blobClient = _containerClient.GetBlobClient(avatarUrl);

            azureResponse = await blobClient.DeleteIfExistsAsync(Azure.Storage.Blobs.Models.DeleteSnapshotsOption.IncludeSnapshots);

            return azureResponse;
        }

        public async Task UpdateAvatarById(User user)
        {
            var sql = $@"Update Users
                             Set AvatarUrl = @AvatarUrl,
                              ModifiedDate = '{DateTime.Now}'
                             Where UserId = @UserId;";

            var paramDetails = new DynamicParameters();

            paramDetails.Add("@AvatarUrl", user.AvatarUrl);
            paramDetails.Add("@UserId", user.UserId);
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var retVal = await connection.ExecuteScalarAsync<Guid>(sql, paramDetails);
            }
        }

        public async Task UpdateLoginDate(string id)
        {
            var sql = $@"Update Users
                             Set LastLoginDate = '{DateTime.Now}',
                              ModifiedDate = '{DateTime.Now}'
                             Where UserId = @UserId;";

            var paramDetails = new DynamicParameters();
            paramDetails.Add("@UserId", id);
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var retVal = await connection.ExecuteScalarAsync<Guid>(sql, paramDetails);
            }
        }

        public async Task UpdateBiometrics(User model)
        {
            var sql = $@"Update Users
                             Set BiometricsEnabled = @BiometricsEnabled,
                              ModifiedDate = '{DateTime.Now}'
                              Where UserId = @UserId;";

            var paramDetails = new DynamicParameters();

            paramDetails.Add("@BiometricsEnabled", model.BiometricsEnabled);
            paramDetails.Add("@UserId", model.UserId);
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var retVal = await connection.ExecuteScalarAsync<object?>(sql, paramDetails);
            }
        }

        public async Task UpdateLocation(User model)
        {
            var sql = $@"Update Users
                             Set LocationEnabled = @LocationEnabled,
                              ModifiedDate = '{DateTime.Now}'
                              Where UserId = @UserId;";

            var paramDetails = new DynamicParameters();

            paramDetails.Add("@LocationEnabled", model.LocationEnabled);
            paramDetails.Add("@UserId", model.UserId);
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var retVal = await connection.ExecuteScalarAsync<object?>(sql, paramDetails);
            }
        }

        public async Task UpdateNotifications(User model)
        {
            var sql = $@"Update Users
                             Set NotificationsEnabled = @NotificationsEnabled,
                              ModifiedDate = '{DateTime.Now}'
                              Where UserId = @UserId;";

            var paramDetails = new DynamicParameters();

            paramDetails.Add("@NotificationsEnabled", model.NotificationsEnabled);
            paramDetails.Add("@UserId", model.UserId);
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var retVal = await connection.ExecuteScalarAsync<object?>(sql, paramDetails);
            }
        }

        public async Task<User> DisableUser(User user)
        {
            user.ModifiedDate = DateTime.Now;
            user.IsActive = "False";
            var imageRet = DeleteProfileImage(user.AvatarUrl);

            await UpdateUserProfile(user);
            return user;
        }
    }
}
