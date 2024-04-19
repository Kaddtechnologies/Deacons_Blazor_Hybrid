using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deacons.Hybrid.Shared.Models;

[Table("WeeklyAssignments")]
public class AssignmentModel
{
    [Key, Required]
    [Dapper.Contrib.Extensions.ExplicitKey]
    public Guid AssignmentId { get; set; }
    [ForeignKey("PostLocations.PostLocationId")]
    public Guid PostLocationId { get; set; }
    [ForeignKey("Posts.PostId")]
    public Guid PostId { get; set; }
    [ForeignKey("Users.UserId")]
    public Guid UserId { get; set; }
    [Required]
    public DateTime AssignmentDate { get; set; }
    public DateTime? UploadDateTime { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
}
