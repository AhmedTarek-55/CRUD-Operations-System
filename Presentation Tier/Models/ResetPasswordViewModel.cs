using System.ComponentModel.DataAnnotations;

namespace Presentation_Tier.Models
{
	public class ResetPasswordViewModel
	{
		[Required]
		[MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
		public string Password { get; set; }

		[Required]
		[MinLength(8)]
		[Compare(nameof(Password), ErrorMessage = "Passwords are not matching")]
		public string ConfirmPassword { get; set; }

		public string EmailAddress { get; set; }
		public string Token { get; set; }
	}
}
