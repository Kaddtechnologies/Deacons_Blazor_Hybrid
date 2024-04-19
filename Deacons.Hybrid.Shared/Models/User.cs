using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deacons.Hybrid.Shared.Models
{
    [Table("Users")]
    public class User
    {
        [Key, Required]
        [Dapper.Contrib.Extensions.ExplicitKey]
        public Guid UserId { get; set; }
        [ForeignKey("CampusId")]
        public Guid? CampusId { get; set; }
        [ForeignKey("TeamId")]
        public Guid? TeamId { get; set; }
        [ForeignKey("TitleId")]
        public Guid? TitleId { get; set; }
        [ForeignKey("PostLocationId")]
        public Guid? PostLocationId { get; set; }
        [ForeignKey("PostId")]
        public Guid? PostId { get; set; }
        [ForeignKey("DeaconCodeId")]
        public Guid? DeaconCodeId { get; set; }
        public string? DeaconTitle { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Initials { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? State { get; set; } = string.Empty;
        public string? Zip { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; } = string.Empty;
        public string? IsActive { get; set; } = "True";
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string DeaconPosition { get; set; } = string.Empty;
        public string TeamName { get; set; } = string.Empty;
        public string PostLocation { get; set; } = string.Empty;
        public string AboutMe { get; set; } = string.Empty;
        public DateTime? PostLocationDate { get; set; }
        public bool? BiometricsEnabled { get; set; } = false;
        public bool? NotificationsEnabled { get; set; } = false;
        public string? NotificationSelection { get; set; } = string.Empty;
        public string? EmergencyContactName { get; set; } = string.Empty;
        public string? EmergencyContactPhone { get; set; } = string.Empty;
        public string? EmergencyContactRelationship { get; set; } = string.Empty;
        public string? AdditionalNotes { get; set; } = string.Empty;
        public string? InstalledVersion { get; set; } = string.Empty;
        public bool? LocationEnabled { get; set; } = false;
        [Dapper.Contrib.Extensions.Write(false)]
        public string? TopicCategory { get; set; } = string.Empty;
        [Dapper.Contrib.Extensions.Write(false)]
        public string? FirebaseDeviceId { get; set; }

        [Dapper.Contrib.Extensions.Write(false)]
        public IList<AttendanceModel>? UserCheckIns { get; set; }
    }
}
