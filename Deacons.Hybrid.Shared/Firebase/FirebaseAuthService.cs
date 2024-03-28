using FirebaseAdmin.Auth;
using FirebaseAdmin;
using User = Deacons.Hybrid.Shared.Models.User;

namespace Deacons.Hybrid.Shared.Firebase
{
    public static  class FirebaseAuthService
    {
        public static async Task<string> CreateUserAsync(string id, string email, string name, string phoneNumber)
        {
            var firebaseAuth = FirebaseAuth.GetAuth(FirebaseInitializer.FirebaseInstance);
            var password = "tph2024";
            try
            {
                if (await firebaseAuth.GetUserByEmailAsync(email) == null)
                {
                    var user = await firebaseAuth.CreateUserAsync(new UserRecordArgs
                    {
                        Email = email,
                        Password = password,
                        PhoneNumber = $@"+1 {phoneNumber}",
                        DisplayName = name,
                        Disabled = false,
                        Uid = id,
                    });
                }
            }
            catch (FirebaseException error)
            {
                switch (error.ErrorCode)
                {
                    case ErrorCode.AlreadyExists:
                        Console.WriteLine($@"Email address ${email} already in use.");
                        break;
                    case ErrorCode.InvalidArgument:
                        Console.WriteLine($@"Email address ${email} invalid.");
                        break;
                    case ErrorCode.NotFound:
                        Console.WriteLine($@"Email address ${email} not found. Creating new user");

                        try
                        {
                            var user = await firebaseAuth.CreateUserAsync(new UserRecordArgs
                            {
                                Email = email,
                                Password = password,
                                PhoneNumber = $@"+1 {phoneNumber}",
                                DisplayName = name,
                                Disabled = false,
                                Uid = id,
                            });
                        }
                        catch (Exception)
                        {
                        }
                        break;
                    case ErrorCode.PermissionDenied:
                        Console.WriteLine($@"Operation not allowed ${email}.");
                        break;
                    default:
                        Console.WriteLine(error.Message);
                        break;
                }
            }

            return id;
        }
      
        public static async Task<object?> GetAllUsersAsync()
        {
            var firebaseAuth = FirebaseAuth.GetAuth(FirebaseInitializer.FirebaseInstance);

            var allUsers = firebaseAuth.ListUsersAsync(null).GetAsyncEnumerator();
            List<ExportedUserRecord> records = new List<ExportedUserRecord>();
            while (await allUsers.MoveNextAsync())
            {
                records.Add(allUsers.Current);
            }


            return records;
        }
        public static async Task<string> DisableUserAsync(User userModel)
        {
            var firebaseAuth = FirebaseAuth.GetAuth(FirebaseInitializer.FirebaseInstance);
            try
            {
                var user = await firebaseAuth.UpdateUserAsync(new UserRecordArgs
                {
                    Disabled = true,
                    Uid = userModel.UserId.ToString(),
                });
            }
            catch (FirebaseException error)
            {
                Console.WriteLine(error.Message);
                return "Failure" + error.Message;
            }
            return "Success";
        }
        public static async Task GeneratePasswordResetLinkAsync(string email)
        {
            var firebaseAuth = FirebaseAuth.GetAuth(FirebaseInitializer.FirebaseInstance);
            await firebaseAuth.GeneratePasswordResetLinkAsync(email);
        }

    }
}
