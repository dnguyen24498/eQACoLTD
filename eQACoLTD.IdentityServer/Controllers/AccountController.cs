using EmailService;
using eQACoLTD.Data.Entities;
using eQACoLTD.ViewModel.System.Account.Handlers;
using eQACoLTD.ViewModel.System.Account.Queries;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eQACoLTD.IdentityServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IIdentityServerInteractionService _identityServerInteractionService;
        private readonly IEmailSender _emailSender;
        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IIdentityServerInteractionService identityServerInteractionService, IConfiguration configuration,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identityServerInteractionService = identityServerInteractionService;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginAccountRequest() { ReturnUrl = returnUrl });
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterAccountRequest() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginAccountRequest loginRequestViewModel)
        {
            if (!ModelState.IsValid) return View(loginRequestViewModel);
            var checkUser = await _userManager.FindByEmailAsync(loginRequestViewModel.UserName);
            var checkPassword = await _userManager.CheckPasswordAsync(checkUser, loginRequestViewModel.Password);
            if (checkUser == null || !checkPassword)
            {
                ViewBag.LoginError = "Sai tên đăng nhập hoặc mật khẩu";
                return View(loginRequestViewModel);
            }
            await _signInManager.SignOutAsync();
            var signinResult = await _signInManager.PasswordSignInAsync(checkUser, loginRequestViewModel.Password, 
                loginRequestViewModel.RememberLogin,false);
            if (signinResult.IsLockedOut)
            {
                ViewBag.LoginError = "Tài khoản bị khóa, hãy liên hệ với quản trị viên để được hỗ trợ";
                return View(loginRequestViewModel);
            }
            if (!signinResult.Succeeded)
            {
                ViewBag.LoginError = "Tài khoản chưa xác nhận mail";
                return View(loginRequestViewModel);
            }
            return Redirect(loginRequestViewModel.ReturnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterAccountRequest registerRequestViewModel)
        {
            if (!ModelState.IsValid) return View(registerRequestViewModel);
            var checkUser = await _userManager.FindByNameAsync(registerRequestViewModel.UserName);
            if (checkUser != null)
            {
                ViewBag.RegisterError = $"Tài khoản {registerRequestViewModel.UserName} đã tồn tại!";
                return View(registerRequestViewModel);
            }
            var checkEmail = await _userManager.FindByEmailAsync(registerRequestViewModel.Email);
            if (checkEmail != null)
            {
                ViewBag.RegisterError = $"Email {registerRequestViewModel.Email} đã được sử dụng!";
                return View(registerRequestViewModel);
            }
            AppUser newUser = new AppUser(registerRequestViewModel.UserName) { Email = registerRequestViewModel.Email };
            var createUserResult = await _userManager.CreateAsync(newUser, registerRequestViewModel.Password);
            if (!createUserResult.Succeeded)
            {
                ViewBag.RegisterError = $"Có lỗi khi tạo tài khoản, xin vui lòng liên hệ quản trị viên!";
                return View(registerRequestViewModel);
            }
            await SendEmailConfirmationLink(newUser, registerRequestViewModel.ReturnUrl);
            var context = await _identityServerInteractionService
                .GetAuthorizationContextAsync(registerRequestViewModel.ReturnUrl);
            if (context != null && string.Equals(context.ClientId, "mvc_admin"))
                return Redirect(_configuration["AdminHost"]);
            return Redirect(_configuration["ClientHost"]);
        }

        private async Task SendEmailConfirmationLink(AppUser user, string returnUrl)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account",
            new { token, email = user.Email, returnUrl }, Request.Scheme);
            var message = new Message(new string[] { user.Email },
            "Confirmation email link", confirmationLink, null);
            await _emailSender.SendEmailAsync(message);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return RedirectToAction(nameof(Error), new { returnUrl });
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
                return View(nameof(ConfirmEmail));
            else
                return RedirectToAction(nameof(Error), new { returnUrl });
        }

        [HttpGet]
        public IActionResult Error(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _identityServerInteractionService.GetLogoutContextAsync(logoutId);
            if (string.IsNullOrEmpty(logoutRequest.PostLogoutRedirectUri))
            {
                return RedirectToAction("Login", "Account");
            }
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }

        [HttpGet]
        public IActionResult ForgotPassword(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest forgotPasswordRequest,
            string returnUrl)
        {
            if (!ModelState.IsValid) return View(forgotPasswordRequest);
            var user = await _userManager.FindByEmailAsync(forgotPasswordRequest.Email);
            if (user == null) return RedirectToAction(nameof(ForgotPasswordConfirmation));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email, returnUrl },
                Request.Scheme);
            var message = new Message(new string[] { user.Email }, "Reset password token", callback, null);
            await _emailSender.SendEmailAsync(message);
            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var model = new ResetPasswordRequest { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest resetPasswordRequest, string returnUrl)
        {
            if (!ModelState.IsValid) return View(resetPasswordRequest);

            var user = await _userManager.FindByEmailAsync(resetPasswordRequest.Email);
            if (user == null)
                RedirectToAction(nameof(ResetPasswordConfirmation), new { returnUrl });

            var resetPassResult = await _userManager.ResetPasswordAsync(user,
                resetPasswordRequest.Token, resetPasswordRequest.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View();
            }
            return RedirectToAction(nameof(ResetPasswordConfirmation), new { returnUrl });
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        private async Task HandleLockout(string email, string returnUrl)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var forgotPassLink = Url.Action(nameof(ForgotPassword), "Account",
                new { returnUrl }, Request.Scheme);
            var content = string.Format(@"Your account is locked out, 
                     to reset your password, please click this link: {0}", forgotPassLink);

            var message = new Message(new string[] { user.Email },
                "Locked out account information", content, null);
            await _emailSender.SendEmailAsync(message);

            ModelState.AddModelError("", "The account is locked out");
        }
    }
}
