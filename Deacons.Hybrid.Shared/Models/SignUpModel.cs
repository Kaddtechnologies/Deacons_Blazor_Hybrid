using System.ComponentModel.DataAnnotations;

namespace Deacons.Hybrid.Shared.Models
{
    public class SignUpModel
    {
        [Required, EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required, Compare(nameof(Password), ErrorMessage = "The passwords didn't match.")]
        public required string ConfirmPassword { get; set; }
    }
}
