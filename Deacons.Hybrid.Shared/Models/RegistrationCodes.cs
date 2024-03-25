using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deacons.Hybrid.Shared.DBModels
{
    [Table("DeaconCodes")]
    public class RegistrationCodes
    {
        [Key]
        [Dapper.Contrib.Extensions.ExplicitKey]
        public string CodeId { get; set; }
        
        public string? Code { get; set; }

        public Guid? UserId { get; set; }

        public DateTime? LastUsedDateTime { get; set; }
    }
}
