﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.DAl.Data;
using Project.PL.Helpers;
using Project.PL.ViewModel;
using Project_.DAL.Models;
using System;
using System.Threading.Tasks;

namespace Project.PL.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
            this.roleManager = roleManager;
        }

        public IActionResult Register() => View();

        [HttpPost]

        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);
            }

            bool isFirstUser = false;
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                isFirstUser = true;
            }

            var adminUsers = await userManager.GetUsersInRoleAsync("Admin");

            if (isFirstUser || adminUsers.Count == 0)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
            else
            {
                if (!await roleManager.RoleExistsAsync("User"))
                {
                    await roleManager.CreateAsync(new IdentityRole("User"));
                }
                await userManager.AddToRoleAsync(user, "User");
            }

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmEmailLink = Url.Action("ConfirmEmail", "Accounts", new { UserId = user.Id, Token = token }, protocol: HttpContext.Request.Scheme);

            var email = new Email
            {
                Subject = "Confirm your account",
                Recivers = model.Email,
                Body = $"Please confirm your email by clicking the following link: {confirmEmailLink}"
            };

            // Create a cart for the user
            var cart = new Cart
            {
                ApplicationUserId = user.Id,
                CreatedAt = DateTime.Now
            };
            context.Carts.Add(cart);
            await context.SaveChangesAsync();

            // Send confirmation email
            EmailSe.SendEmail(email);

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token)) return RedirectToAction("Error", "Home");

            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return RedirectToAction("Error", "Home");

            var result = await userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded ? RedirectToAction(nameof(Login)) : RedirectToAction("Error", "Home");
        }

        public IActionResult Login() => View();
       

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
                {
                    var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded) return RedirectToAction("Index", "Users");
                }

                TempData["LoginError"] = "Invalid email or password.";
                return RedirectToAction("Login");
            }
            catch (InvalidOperationException ex)
            {
                TempData["LoginError"] = "An unexpected error occurred. Please contact support.";
                return RedirectToAction("Login");
            }
        }




        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var resetLink = Url.Action("ResetPassword", "Accounts", new { email = model.Email, Token = token }, protocol: HttpContext.Request.Scheme);

                // البريد الإلكتروني بتصميم HTML
                var email = new Email
                {
                    Subject = "Reset Your Password",

                    Recivers = model.Email,
                    Body = $@"
            <html>
            <body style='font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;'>
                <div style='max-width: 600px; margin: 0 auto; background-color: white; padding: 20px; border-radius: 8px; box-shadow: 0 4px 8px rgba(0,0,0,0.1);'>
                    <h2>Password Reset Request</h2>
                    <p>We received a request to reset your password. To proceed, please click the button below:</p>
                    <a href='{resetLink}' style='background-color: #4CAF50; color: white; padding: 14px 20px; text-align: center; text-decoration: none; display: inline-block; font-size: 16px; border-radius: 5px;'>Reset Password</a>
                    <p>If you did not request a password reset, please ignore this email.</p>
                    <p>Regards,<br>Your Website Team</p>
                </div>
            </body>
            </html>"
                };

                // إرسال البريد الإلكتروني
                EmailSe.SendEmail(email);
            }

            return View(); // يمكنك عرض رسالة للمستخدم بعد إرسال البريد
        }


        public IActionResult ResetPassword(string email, string token)
        {
            var model = new ResetPaw { Email = email, Token = token }; // Pre-fill the model if necessary
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPaw model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
            }

            ModelState.AddModelError(string.Empty, "Error resetting password.");
            return View(model);
        }
    }
}
