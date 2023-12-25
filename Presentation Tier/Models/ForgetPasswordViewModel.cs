using System.ComponentModel.DataAnnotations;

namespace Presentation_Tier.Models
{
	public class ForgetPasswordViewModel
	{
		[Required]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string EmailAddress { get; set; }
	}
}
