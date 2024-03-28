using Deacons.Hybrid.Shared.Models;

namespace Deacons.Hybrid.Shared.Interface
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(EmailModel email, List<Group> groupsList);

        bool IsValidEmail(string EmailName);
    }
}
