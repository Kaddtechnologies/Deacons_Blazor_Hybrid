using Deacons.Hybrid.Shared.Models;

namespace Deacons.Hybrid.Shared.Data
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(EmailModel email, List<Group> groupsList);
        bool IsValidEmail(string EmailName);
    }
}
