using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deacons.Hybrid.Shared.Models
{

    [Table("Attendance")]
    public class AttendanceModel
    {

        [Key, Required]
        [Dapper.Contrib.Extensions.ExplicitKey]
        public Guid AttendanceId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? EventId { get; set; }
        public Guid? PostLocationId { get; set; }
        public Guid? PostId { get; set; }
        public string? PostLocation { get; set; }
        public string? PostName { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public bool? IsOnline { get; set; }
        public string? CheckInLocation { get; set; }
        [Dapper.Contrib.Extensions.Write(false)]
        public string PostLocationIdString
        {
            get { return PostLocationId?.ToString("N"); }
            set { PostLocationId = new Guid(value); }
        }
        [Dapper.Contrib.Extensions.Write(false)]
        public string PostIdString
        {
            get { return PostId?.ToString("N"); }
            set { PostId = new Guid(value); }
        }
    }

 
}
