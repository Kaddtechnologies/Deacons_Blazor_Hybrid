using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deacons.Hybrid.Shared.Models
{
    [Table("PostLocations")]
    public class PostLocationModel
    {

        [Key, Required]
        [Dapper.Contrib.Extensions.ExplicitKey]
        public Guid PostLocationId { get; set; }
        public string? PostLocationName { get; set; }

        [Dapper.Contrib.Extensions.Write(false)]
        public string PostLocationIdString
        {
            get { return PostLocationId.ToString("N"); }
            set { PostLocationId = new Guid(value); }
        }
    }
}
