using Deacons.Hybrid.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using Deacons.Hybrid.Shared.Interface;

namespace Deacons.Hybrid.Shared.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly MailSetting _mailConfig;
        private static string _mailResponse;
        //private Response response;
        public EmailService(IConfiguration configuration, MailSetting settings)
        {
            _configuration = configuration;
           _mailConfig = settings;
        }

        public async Task<string> SendEmailAsync(EmailModel email, List<Group> groupsList)
        {

            try
            {
                var apiKey = _mailConfig.SendGridApiKey;
                var client = new SendGridClient(apiKey);
                var msg = new SendGridMessage
                {
                    From = new EmailAddress("info@kaddatechnologies.com", _mailConfig.DisplayName),
                    Subject = email.Subject,
                    PlainTextContent = email.Body,
                    HtmlContent = email.Body,

                };
                foreach (string EmailName in email.To)
                {
                    if (EmailName.Contains("@"))
                        msg.AddTo(new EmailAddress(EmailName));
                    else
                    {

                        var lst = groupsList.Where(x => x.GroupName == EmailName).FirstOrDefault();
                        if (lst != null && lst.Emails.Count > 0)
                        {
                            foreach (var item in lst.Emails)
                            {
                                msg.AddTo(new EmailAddress(item.emailaddress));
                                // response = await client.SendEmailAsync(msg);

                            };
                        }
                    }
                }
                var response = await client.SendEmailAsync(msg);
                if (response.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception($"Failed to send email. Status code: {response.StatusCode}");
                }
                return _mailResponse;
            }
            catch (Exception ex)
            {
                throw new Exception($"Some thing went wrong to send email. : {ex.Message}");
            }

        }
        public bool IsValidEmail(string EmailName)
        {
            return new EmailAddressAttribute().IsValid(EmailName);
        }

    }
}
