using IKIA.BLL.Common.Services.Emails;
using IKIA.DAL.Models.Identity;
using IKIA.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IKIA.PL.Controllers
{
    public class AccountController : Controller
	{
		#region Services

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IEmailService _emailService;
		private readonly IConfiguration _configuration;

		public AccountController(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			IEmailService emailService,
			IConfiguration configuration)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_emailService = emailService;
			_configuration = configuration;
		}

		#endregion

		#region Sign Up

		[HttpGet]    
		public IActionResult SignUp()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel userVM)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var userfind = await _userManager.FindByNameAsync(userVM.UserName);

			if (userfind is { })
			{
				ModelState.AddModelError(string.Empty, "This username is already in use for another account");
				return View(userVM);
			}
			else if (userfind == null)
			{
				var user = new ApplicationUser()
				{
					FName = userVM.FirstName,
					LName = userVM.LastName,
					UserName = userVM.UserName,
					Email = userVM.Email,
					IsAgree = userVM.IsAgree,
				};

				var result = await _userManager.CreateAsync(user, userVM.Password);

				if (result.Succeeded)
					return RedirectToAction(nameof(SignIn));
				else
				{
					foreach (var error in result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);
				}
			}
			return View(userVM);
		}

		#endregion

		#region Sign In

		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel userVM)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var user = await _userManager.FindByEmailAsync(userVM.Email);
			if (user is { })
			{
				var flag = await _userManager.CheckPasswordAsync(user, userVM.Password);
				if (flag)
				{
					var result = await _signInManager.PasswordSignInAsync(user, userVM.Password, userVM.RememberMe, true);
					
					if (result.IsNotAllowed)
						ModelState.AddModelError(string.Empty, "Your account is not confirmed Yet !!");

					if (result.IsLockedOut)
						ModelState.AddModelError(string.Empty, "Your Account is Locked");

					if (result.Succeeded)
						return RedirectToAction("Index", "Home");
					else if (!result.Succeeded)
						ModelState.AddModelError(string.Empty, "Email or Password is Wrong");
				}
			}
			else
			{
				ModelState.AddModelError(string.Empty, "User not found");
			}
			return View(userVM);
		}

		#endregion

		#region Sign Out

		public async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("SignIn");
		}

		#endregion

		#region Forget Password and Send Reset Password Email

		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SendResetPasswordEmail(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is { })
				{
					var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);  // unique 

					var resetPasswordURL = Url.Action("ResetPassword", "Account", new { email = model.Email, token = resetPasswordToken } , "https" ,"localhost:7049");
					
					await _emailService.SendAsync(
						_configuration["EmailSettings:SenderEmail"],
						model.Email,
						"Reset your password",
						resetPasswordURL);

					return RedirectToAction(nameof(CheckYourInbox));
				}
				ModelState.AddModelError(string.Empty, "No user with this email !!");
				return View(model);
			}
			return View(model);
		}


		public IActionResult CheckYourInbox()
		{
			return View();
		}

		#endregion

		#region Reset Password

		[HttpGet]
		public IActionResult ResetPassword(string email, string token)
		{
			TempData["Email"] = email;
			TempData["Token"] = token;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var email = TempData["email"] as string;
				var token = TempData["Token"] as string;

				var user = await _userManager.FindByEmailAsync(email);

				if (user != null)
				{
					var result = await _userManager.ResetPasswordAsync(user,token,model.NewPassword);

					if (!result.Succeeded)
						foreach (var error in result.Errors)
							Console.WriteLine($"Error: {error.Description}");

					return RedirectToAction(nameof(SignIn));
				}
				ModelState.AddModelError("", "URL is not valid !!!! ");
			}
			return View(model);
		}

		#endregion
	}
}
