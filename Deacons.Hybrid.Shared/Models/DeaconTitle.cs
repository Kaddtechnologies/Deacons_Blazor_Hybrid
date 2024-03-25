using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deacons.Hybrid.Shared.DBModels
{
    [Table("DeaconTitle")]
    public class DeaconTitleModel
    {
        [Key,Required]
        [Dapper.Contrib.Extensions.ExplicitKey]
        public Guid TitleId { get; set; }
        public string? DeaconTitle { get; set; }
        public string? DeaconPosition { get; set; }
    }
}
