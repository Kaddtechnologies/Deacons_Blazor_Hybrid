using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deacons.Hybrid.Shared.DBModels
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
        [ForeignKey("DeaconCodeId")]
        public Guid? DeaconCodeId { get; set; }
        public string? DeaconTitle { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Initials {  get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? AvatarUrl { get; set; }
        public string? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string DeaconPosition { get;  set; } = string.Empty;
        public string TeamName { get; set; } = string.Empty;
        public string PostLocation { get; set; } = string.Empty;
        public string AboutMe { get; set; } = string.Empty;
        public DateTime? PostLocationDate { get; set; }
    }
}
