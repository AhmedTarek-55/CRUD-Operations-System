using System.ComponentModel.DataAnnotations;

namespace Presentation_Tier.Models
{
	public class SignInViewModel
	{
		[Required]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string EmailAddress { get; set; }

		[Required]
		[MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
		public string Password { get; set; }

		public bool RememberMe { get; set; }
	}
}
