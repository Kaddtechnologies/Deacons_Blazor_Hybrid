using Deacons.Hybrid.Shared.Models;
using Microsoft.AspNetCore.Http;

namespace Deacons.Hybrid.Shared.Interface
{
    public interface IUsersService
    {
        Task<IEnumerable<TeamUserModel>> GetAll();
        Task<object?> GetAllUsers();
        Task Create(User user);
        Task<RegistrationCodes> GetRegistrationCodeDetails(Guid registrationCode);
        Task UpdateRegistrationCodeDetails(RegistrationCodes registration, User user);
        Task<object?> UploadImages(IFormFile formFile);
        Task<bool> DeleteProfileImage(string avatarUrl);
        Task UpdateAvatarById(User user);
        Task UpdateLoginDate(string id);
        Task<object?> UpdateUserProfile(User newUser);
        Task UpdateBiometrics(User model);
        Task UpdateLocation(User model);
        Task UpdateNotifications(User model);
        Task<User> DisableUser(User user);
    }
}
