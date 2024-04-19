using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deacons.Hybrid.Shared.Models
{

    [Table("ChurchEvent")]
    public class ChurchEventsModel
    {
        [Key, Required]
        [Dapper.Contrib.Extensions.ExplicitKey]
        public Guid EventId { get; set; }
        public string? EventName { get; set; }
    }
}
