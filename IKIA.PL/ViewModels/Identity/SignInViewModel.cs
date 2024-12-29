using System.ComponentModel.DataAnnotations;

namespace IKIA.PL.ViewModels.Identity
{
    public class SignInViewModel
	{
		[EmailAddress]
		public string Email { get; set; } = null!;

		
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;


        public bool RememberMe { get; set; }       // used for storing the token in the cookies storage of the browser
    }
}
