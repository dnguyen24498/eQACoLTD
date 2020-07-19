using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eQACoLTD.Data.Entities;
using eQACoLTD.ViewModel.System.Account.Handlers;
using eQACoLTD.ViewModel.System.Account.Queries;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.IdentityServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IIdentityServerInteractionService _identityServerInteractionService;
        private readonly IMapper _mapper;
        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, 
            IIdentityServerInteractionService identityServerInteractionService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identityServerInteractionService = identityServerInteractionService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginRequest() { ReturnUrl = returnUrl });
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterRequest() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequestViewModel)
        {
            if (!ModelState.IsValid) return View(loginRequestViewModel);
            var checkUser = await _userManager.FindByNameAsync(loginRequestViewModel.UserName);
            if (checkUser == null)
            {
                loginRequestViewModel.LoginSucceeded = false;
                loginRequestViewModel.Error = "Tên đăng nhập hoặc mật khẩu không đúng!";
                return View(loginRequestViewModel);
            }
            var context = await _identityServerInteractionService
               .GetAuthorizationContextAsync(loginRequestViewModel.ReturnUrl);
            if (string.Equals(context.ClientId, "mvc_admin"))
            {
                var roles = await _userManager.GetRolesAsync(checkUser);
                if (!roles.Contains("Admin"))
                {
                    loginRequestViewModel.LoginSucceeded = false;
                    loginRequestViewModel.Error = "Không đủ quyền truy cập trang quản trị, hãy liên hệ với quản trị viên để được cấp quyền!";
                    return View(loginRequestViewModel);
                }
            }
            await _signInManager.SignOutAsync();
            var signinResult = await _signInManager.PasswordSignInAsync(checkUser, loginRequestViewModel.Password, false, false);
            if (!signinResult.Succeeded)
            {
                loginRequestViewModel.LoginSucceeded = false;
                loginRequestViewModel.Error = "Tên đăng nhập hoặc mật khẩu không đúng!";
                return View(loginRequestViewModel);
            }
            return Redirect(loginRequestViewModel.ReturnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest registerRequestViewModel)
        {
            if (!ModelState.IsValid) return View(registerRequestViewModel);
            var checkUser = await _userManager.FindByNameAsync(registerRequestViewModel.UserName);
            if (checkUser != null)
            {
                registerRequestViewModel.RegisterSucceeded = false;
                registerRequestViewModel.Error = $"Tên tài khoản {registerRequestViewModel.UserName} đã tồn tại!";
                return View(registerRequestViewModel);
            }
            var checkEmail = await _userManager.FindByEmailAsync(registerRequestViewModel.Email);
            if (checkEmail != null)
            {
                registerRequestViewModel.RegisterSucceeded = false;
                registerRequestViewModel.Error = $"Email {registerRequestViewModel.Email} đã được sử dụng!";
                return View(registerRequestViewModel);
            }
            AppUser newUser = _mapper.Map<AppUser>(registerRequestViewModel);
            var createUserResult = await _userManager.CreateAsync(newUser, registerRequestViewModel.Password);
            if (!createUserResult.Succeeded)
            {
                registerRequestViewModel.RegisterSucceeded = false;
                registerRequestViewModel.Error = $"Có lỗi khi tạo tài khoản, xin vui lòng liên hệ quản trị viên!";
                return View(registerRequestViewModel);
            }
            var signinResult = await _signInManager.PasswordSignInAsync(newUser.UserName, registerRequestViewModel.Password, false, false);
            if (!signinResult.Succeeded) return View(registerRequestViewModel);
            if (string.IsNullOrEmpty(registerRequestViewModel.ReturnUrl))
            {
                //Trường hợp không có Return Url
            }
            return Redirect(registerRequestViewModel.ReturnUrl);

        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var logoutRequest = await _identityServerInteractionService.GetLogoutContextAsync(logoutId);
            if (string.IsNullOrEmpty(logoutRequest.PostLogoutRedirectUri))
            {
                return RedirectToAction("Index", "Home");
            }

            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }

    }
}
