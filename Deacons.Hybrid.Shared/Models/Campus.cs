using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deacons.Hybrid.Shared.Models
{
    [Table("Campus")]
    public class Campus
    {
        [Key, Required]
        [Dapper.Contrib.Extensions.ExplicitKey]
        public Guid CampusId { get; set; }
        public string CampusName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
