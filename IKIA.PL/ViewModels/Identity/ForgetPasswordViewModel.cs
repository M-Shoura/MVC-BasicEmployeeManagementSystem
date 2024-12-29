using System.ComponentModel.DataAnnotations;

namespace IKIA.PL.ViewModels.Identity
{
    public class ForgetPasswordViewModel
	{
		[EmailAddress]
		public string Email { get; set; } = null!;
	}
}
