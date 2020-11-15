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
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using TwoStepDto = eQACoLTD.ViewModel.System.Account.Queries.TwoStepDto;

namespace eQACoLTD.IdentityServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IIdentityServerInteractionService _identityServerInteractionService;
        private readonly IEmailSender _emailSender;
        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,IClientStore clientStore, IAuthenticationSchemeProvider schemeProvider,
            IIdentityServerInteractionService identityServerInteractionService, IConfiguration configuration,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identityServerInteractionService = identityServerInteractionService;
            _configuration = configuration;
            _emailSender = emailSender;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var vm = await BuildLoginViewModelAsync(returnUrl);
            if (vm.IsExternalLoginOnly)
            {
                // we only have one option for logging in and it's an external provider
                return RedirectToAction("Challenge", "External", new { scheme = vm.ExternalLoginScheme, returnUrl });
            }
            return View(vm);
        }

        private async Task<LoginAccountDto> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _identityServerInteractionService.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
            {
                var local = context.IdP == IdentityServer4.IdentityServerConstants.LocalIdentityProvider;

                // this is meant to short circuit the UI and only trigger the one external IdP
                var vm = new LoginAccountDto()
                {
                    EnableLocalLogin = local,
                    ReturnUrl = returnUrl,
                    UserName = context?.LoginHint,
                };

                if (!local)
                {
                    vm.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
                }

                return vm;
            }
            var schemes = await _schemeProvider.GetAllSchemesAsync();

            var providers = schemes
                .Where(x => x.DisplayName != null)
                .Select(x => new ExternalProvider
                {
                    DisplayName = x.DisplayName ?? x.Name,
                    AuthenticationScheme = x.Name
                }).ToList();

            var allowLocal = true;
            if (context?.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;

                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    {
                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                    }
                }
            }

            return new LoginAccountDto()
            {
                RememberLogin = true,
                EnableLocalLogin = allowLocal && true,
                ReturnUrl = returnUrl,
                UserName = context?.LoginHint,
                ExternalProviders = providers.ToArray()
            };
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterAccountDto() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginAccountDto loginDtoViewModel)
        {
            if (!ModelState.IsValid) return View(loginDtoViewModel);
            var checkUser = await _userManager.FindByEmailAsync(loginDtoViewModel.UserName);
            if (checkUser == null)
            {
                checkUser = await _userManager.FindByNameAsync(loginDtoViewModel.UserName);
                if (checkUser == null)
                {
                    ViewBag.LoginError = "Sai tên đăng nhập hoặc mật khẩu";
                    return View(loginDtoViewModel);   
                }
            }
            var checkPassword = await _userManager.CheckPasswordAsync(checkUser, loginDtoViewModel.Password);
            if (!checkPassword)
            {
                ViewBag.LoginError = "Sai tên đăng nhập hoặc mật khẩu";
                return View(loginDtoViewModel);   
            }

            if (!checkUser.EmailConfirmed)
            {
                ViewBag.LoginError = "Tài khoản chưa xác nhận mail";
                return View(loginDtoViewModel); 
            } 
            await _signInManager.SignOutAsync();
            var signinResult = await _signInManager.PasswordSignInAsync(checkUser, loginDtoViewModel.Password, 
                loginDtoViewModel.RememberLogin,true);
            if (signinResult.IsLockedOut)
            {
                ViewBag.LoginError = "Tài khoản bị khóa, hãy liên hệ với quản trị viên để được hỗ trợ";
                return View(loginDtoViewModel);
            }

            if (signinResult.RequiresTwoFactor)
            {
                return RedirectToAction(nameof(LoginTwoStep),
                    new
                    {
                        Email = checkUser.EmailConfirmed, loginDtoViewModel.RememberLogin, loginDtoViewModel.ReturnUrl
                    });
            }
            return Redirect(loginDtoViewModel.ReturnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> LoginTwoStep(string email,bool rememberLogin,string returnUrl)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return RedirectToAction(nameof(Error), new { returnUrl });
            }
            var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);
            if (!providers.Contains("Email"))
            {
                return RedirectToAction(nameof(Error), new { returnUrl });
            }
            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
            var message = new Message(new string[] { email }, "Authentication token", token, null);
            await _emailSender.SendEmailAsync(message);
            ViewData["RouteData"] = new Dictionary<string, string>
            {
                { "returnUrl", returnUrl },
                { "email", email }
            };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginTwoStep(TwoStepDto twoStepDto, string returnUrl, string email)
        {
            if (!ModelState.IsValid)
            {
                return View(twoStepDto);
            }
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return RedirectToAction(nameof(Error), new { returnUrl });
            }
            var result = await _signInManager.TwoFactorSignInAsync("Email",
                twoStepDto.TwoFactorCode, twoStepDto.RememberLogin, rememberClient: false);
            if (result.Succeeded)
            {
                return this.Redirect( returnUrl);
            }
            else if (result.IsLockedOut)
            {
                await HandleLockout(email, returnUrl);
                return View(twoStepDto);
            }
            else
            {
                return RedirectToAction(nameof(Error), new {returnUrl});
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterAccountDto registerDtoViewModel)
        {
            if (!ModelState.IsValid) return View(registerDtoViewModel);
            var checkUser = await _userManager.FindByNameAsync(registerDtoViewModel.UserName);
            if (checkUser != null)
            {
                ViewBag.RegisterError = $"Tài khoản {registerDtoViewModel.UserName} đã tồn tại!";
                return View(registerDtoViewModel);
            }
            var checkEmail = await _userManager.FindByEmailAsync(registerDtoViewModel.Email);
            if (checkEmail != null)
            {
                ViewBag.RegisterError = $"Email {registerDtoViewModel.Email} đã được sử dụng!";
                return View(registerDtoViewModel);
            }
            AppUser newUser = new AppUser(registerDtoViewModel.UserName) { Email = registerDtoViewModel.Email };
            var createUserResult = await _userManager.CreateAsync(newUser, registerDtoViewModel.Password);
            if (!createUserResult.Succeeded)
            {
                ViewBag.RegisterError = $"Có lỗi khi tạo tài khoản, xin vui lòng liên hệ quản trị viên!";
                return View(registerDtoViewModel);
            }
            await SendEmailConfirmationLink(newUser, registerDtoViewModel.ReturnUrl);
            var context = await _identityServerInteractionService
                .GetAuthorizationContextAsync(registerDtoViewModel.ReturnUrl);
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
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto,
            string returnUrl)
        {
            if (!ModelState.IsValid) return View(forgotPasswordDto);
            var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
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
            var model = new ResetPasswordDto { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto, string returnUrl)
        {
            if (!ModelState.IsValid) return View(resetPasswordDto);

            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
                RedirectToAction(nameof(ResetPasswordConfirmation), new { returnUrl });

            var resetPassResult = await _userManager.ResetPasswordAsync(user,
                resetPasswordDto.Token, resetPasswordDto.Password);
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
