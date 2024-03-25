using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deacons.Hybrid.Shared.DBModels
{
    [Table("CalendarEvents")]
    public class CalendarEventsModel
    {

        [Key, Required]
        [Dapper.Contrib.Extensions.ExplicitKey]
        public Guid CalendarId { get; set; }
        public string? EventName { get; set; } = string.Empty;
        public string? Organizer { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public string? Capacity { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? BackgroundColor { get; set; } = string.Empty;
        public bool? IsAllDay { get; set; } = false;
        public string? StartTimeZone { get; set; } = string.Empty;
        public string? EndTimeZone { get; set; } = string.Empty;
        public string? RecurrenceRule { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;
        public string? EventAddress { get; set; } = string.Empty;
        public string? ZoomUrl { get; set; } = string.Empty;
    }
}
