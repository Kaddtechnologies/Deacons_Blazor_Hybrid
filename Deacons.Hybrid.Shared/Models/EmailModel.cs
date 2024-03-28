using System.Collections.Generic;
using System.Net.Mail;

namespace Deacons.Hybrid.Shared.Models
{
    public class EmailModel
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public MailAddress From { get; set; }
        public List<string> To { get; set; }
        public string ToHtml { get; set; } = string.Empty;
        public string FromHtml { get; set; }    
        
    }
}
