using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deacons.Hybrid.Shared.Models
{
    public class EventsDataModel
    {
        public string calendarId { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter event name.")]
        public string eventName { get; set; } = string.Empty;
        public string organizer { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string userId { get; set; } = string.Empty;
        public string capacity { get; set; } = string.Empty;
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string backgroundColor { get; set; } = string.Empty;
        public bool isAllDay { get; set; }
        public string startTimeZone { get; set; } = string.Empty;
        public string endTimeZone { get; set; } = string.Empty;
        public string recurrenceRule { get; set; } = string.Empty;
        public string notes { get; set; } = string.Empty;
        public string eventAddress { get; set; } = "Address";
        public string zoomUrl { get; set; } = string.Empty;
    }
}
