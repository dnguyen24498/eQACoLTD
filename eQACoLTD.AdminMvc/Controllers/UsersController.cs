using eQACoLTD.AdminMvc.Services;
using eQACoLTD.ViewModel.System.User.Handlers;
using eQACoLTD.ViewModel.System.User.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace eQACoLTD.AdminMvc.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserApiClient _userApiClient;

        public UsersController(IHttpClientFactory httpClientFactory,
            IUserApiClient accountApiClient)
        {
            _httpClientFactory = httpClientFactory;
            _userApiClient = accountApiClient;
        }

        public async Task<IActionResult> Index(int page=1)
        {
            var result = await _userApiClient.GetUserProfilesPagingAsync(page);
            if (!result.IsSuccess) return Ok(result.Message);
            return View(result.ResultObj);
        }

        [HttpGet]
        public async Task<IActionResult> Roles(string userName)
        {
            var result = await _userApiClient.GetUserRolesAsync(userName);
            if (!result.IsSuccess) return Ok(result.Message);
            return View(result.ResultObj);
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateRoles(UserRolesResponse request)
        {
            var updateRequest = new UpdateUserRoleRequest()
            {
                AddRoleNames = request.NotInRoles,
                DeleteRoleNames = request.InRoles
            };
            var result = await _userApiClient.UpdateUserRolesAsync(request.UserName, updateRequest);
            if(!result.IsSuccess) return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Index));
        }

    }
}
