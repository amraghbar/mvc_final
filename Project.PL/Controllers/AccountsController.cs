using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using Project.DAl.Data;
using Project.PL.Helpers;
using Project.PL.ViewModel;
using Project_.DAL.Models;
using System.Security.Policy;

namespace Project.PL.Controllers
{
	public class AccountsController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}
		public IActionResult Register()
		{
			return View();
		}
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                };


                var x = await userManager.CreateAsync(user, model.Password);
                if (x.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmEmailLink = Url.Action("confirmEmail", "Accounts",
                        new { UserId = user.Id, Token = token }, protocol: HttpContext.Request.Scheme);

                    var email = new Email()
                    {
                        Subject = "Confirm Accounts",
                        Recivers = model.Email,
                        Body = $"Please confirm your email by clicking the following link: {confirmEmailLink}"
                    };

                    EmailSe.SendEmail(email); // Assuming this is the email service you're using
                    return RedirectToAction("Login");
                }
                else
                {
                    // Handle errors, for example:
                    foreach (var error in x.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }

            return View(model);
        }
        public async Task<IActionResult> confirmEmail(string UserId, string Token)
        {
            if (UserId == null || Token == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var user = await userManager.FindByIdAsync(UserId);
            if (user != null)
            {
                var res = await userManager.ConfirmEmailAsync(user, Token);
                if (res.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
            }

            return RedirectToAction("Error", "Home");
        }
        public IActionResult Login() {
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginVM model)
		{
			if (ModelState.IsValid)
			{
				var user = await userManager.FindByEmailAsync(model.Email);
				if (user != null)
				{
					var checkPa = await userManager.CheckPasswordAsync(user, model.Password);
					if (checkPa)
					{
						var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
						if (result.Succeeded)
						{
							return RedirectToAction("Index", "Users");
						}
					}
				}

				ModelState.AddModelError(string.Empty, "Invalid login attempt.");
			}

			return View(model);
		}
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Login");
		}

		public IActionResult ForgetPassword() {
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ForgetPassword(ForgetPassword model) {
			var user = await userManager.FindByEmailAsync(model.Email);
			
			if (ModelState.IsValid) {
				if (user is not null)
				{
					var token= await userManager.GeneratePasswordResetTokenAsync(user);
					var resrtpa = Url.Action("ResetPassword", "Accounts", new {email=model.Email,Token=token},"http","localhost:5254");
					var email = new Email()
					{
						Subject="Reset Password",
						Recivers=model.Email,
						Body= resrtpa,
					};

					EmailSe.SendEmail(email);

				}
			}
			return View();

		}
		public IActionResult ResetPassword(string email,string Token) {

			return View();

		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPaw model) {
			var user = await userManager.FindByEmailAsync(model.Email);
			if (user is not null) { 
			var result=await userManager.ResetPasswordAsync(user,model.Token,model.Password);
				if (result.Succeeded)
				{
					return RedirectToAction(nameof(Login));
				}
			}
			return View(model);
		}




    }
}
