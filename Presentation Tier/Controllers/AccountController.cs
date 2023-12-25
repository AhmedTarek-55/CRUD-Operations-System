using Data_Access_Tier.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation_Tier.Helper;
using Presentation_Tier.Models;

namespace Presentation_Tier.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
			_signInManager = signInManager;
		}


        #region SignUp
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = registerViewModel.EmailAddress,
                    UserName = registerViewModel.EmailAddress.Split('@')[0],
                    IsAgree = registerViewModel.IsAgree
                };

                var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                if (result.Succeeded)
                    return RedirectToAction("Login");

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(registerViewModel);
        }
		#endregion

		#region SignIn
		public IActionResult Login(string? ReturnUrl)
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(SignInViewModel signInViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(signInViewModel.EmailAddress);

                if (user is null)
                    ModelState.TryAddModelError("", "Email Is Not Found");

                var IsCorrectPassword = await _userManager.CheckPasswordAsync(user, signInViewModel.Password);

                if (IsCorrectPassword)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, signInViewModel.Password, signInViewModel.RememberMe, false);

                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");
                }

			}
            return View(signInViewModel);
        }
		#endregion

		#region SignOut
		public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}

		#endregion

		#region ForgetPassword
		public IActionResult ForgetPassword()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.EmailAddress);

                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var resetPassworrdLink = Url.Action("ResetPassword", "Account", new { Email = model.EmailAddress, Token = token }, Request.Scheme);

                    Email email = new Email 
                    {
                        Recipient = model.EmailAddress,
                        Subject = "Reset Your Password",
                        Body = resetPassworrdLink
                    };

                    EmailSettings.SendEmail(email);

                    RedirectToAction("CompleteForgetPassword");
                }

                ModelState.AddModelError("", "Invalid Email Address");
			}

            return View(model);
        }

		public IActionResult CompleteForgetPassword()
		{
			return View();
		}
		#endregion

		#region ResetPassword
		public IActionResult ResetPassword(string email, string token)
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
				var user = await _userManager.FindByEmailAsync(model.EmailAddress);

                if (user is not null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                    if (result.Succeeded)
                        RedirectToAction(nameof(SignIn));

					foreach (var error in result.Errors)
						ModelState.AddModelError(string.Empty, error.Description);
				}
				ModelState.AddModelError("", "Invalid Email Address");
			}
			return View(model);
		}
        #endregion

        #region AccessDenied
        public IActionResult AccessDenied()
        {
            return View();
        } 
        #endregion
    }
}