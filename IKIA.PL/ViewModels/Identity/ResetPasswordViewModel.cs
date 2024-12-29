using System.ComponentModel.DataAnnotations;

namespace IKIA.PL.ViewModels.Identity
{
    public class ResetPasswordViewModel
	{
		[DataType(DataType.Password)]
		public string NewPassword { get; set; } = null!;


		[Display(Name = "Confirm Password")]
		[DataType(DataType.Password)]
		[Compare(nameof(NewPassword), ErrorMessage = "Confirm Password doesn't match with New Password")]
		public string ConfirmPassword { get; set; } = null!;
	}
}
