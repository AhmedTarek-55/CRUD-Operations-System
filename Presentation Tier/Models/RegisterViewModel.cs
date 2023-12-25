using System.ComponentModel.DataAnnotations;

namespace Presentation_Tier.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string EmailAddress { get; set; }

        [Required]
        [MinLength(8, ErrorMessage ="Password must be at least 8 characters")]
        public string Password { get; set; }

        [Required]
        [MinLength(8)]
        [Compare(nameof(Password), ErrorMessage = "Passwords are not matching")]
        public string ConfirmPassword { get; set; }

        [Required]
        public bool IsAgree { get; set; }
    }
}