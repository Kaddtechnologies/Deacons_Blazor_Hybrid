using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Deacons.Hybrid.Shared.Models
{
    [Table("Posts")]
    public class PostModel
    {
        [Key, Required]
        [Dapper.Contrib.Extensions.ExplicitKey]
        public Guid PostId { get; set; }
        [ForeignKey("PostLocations.PostLocationId")]
        public Guid? PostLocationId { get; set; }
        public string PostName { get; set; }
        [Dapper.Contrib.Extensions.Write(false)]
        public string PostIdString
        {
            get { return PostId.ToString("N"); }
            set { PostId = new Guid(value); }
        }

        [Dapper.Contrib.Extensions.Write(false)]
        public string PostLocationIdString
        {
            get { return PostLocationId?.ToString("N"); }
            set { PostLocationId = new Guid(value); }
        }
    }
}
