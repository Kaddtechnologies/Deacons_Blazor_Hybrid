using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deacons.Hybrid.Shared.Models
{
    [Table("CheckIns")]
    public class CheckInsModel
    {

        [Key, Required]
        [Dapper.Contrib.Extensions.ExplicitKey]
        public Guid CheckInId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? EventId { get; set; }
        public Guid? PostLocationId { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public bool? IsOnline { get; set; }
        public string? CheckInLocation { get; set; }
    }
}
