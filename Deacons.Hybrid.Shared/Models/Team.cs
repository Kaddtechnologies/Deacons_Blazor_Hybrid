using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deacons.Hybrid.Shared.DBModels
{
    [Table("Teams")]
    public class Team
    {
        [Key,Required]
        [Dapper.Contrib.Extensions.ExplicitKey]
        public Guid TeamId { get; set; }
        public string? TeamName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
