using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deacons.Hybrid.Shared.Models
{
    public class UserCheckInModel : AttendanceModel
    {
        public Guid UserId { get; set; }
        public string? DeaconTitle { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Guid? TitleId { get; set; }
        public string? Initials { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public DateTime? LastLoginDate { get; set; }
        public string DeaconPosition { get; set; } = string.Empty;
        public string TeamName { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; } = string.Empty;
        public string? City { get; set; }
        public string? State {  get; set; } = string.Empty;
        public string? EventName {  get; set; } = string.Empty;

    }

    public class UserCheckInValidator : AbstractValidator<UserCheckInModel>
    {
        public UserCheckInValidator()
        {
           // RuleFor(x => x.CheckOutTime).NotEmpty().WithMessage("Check-Out Time is Required");
        }
    }
}
