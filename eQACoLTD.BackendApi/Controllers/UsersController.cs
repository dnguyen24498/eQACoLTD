using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eQACoLTD.Application.System.User;
using eQACoLTD.ViewModel.System.User.Handlers;
using eQACoLTD.ViewModel.System.User.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("profile")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetUserProfile()
        {
            var userName = User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _userService.GetUserProfileAsync(userName);
            if (!result.IsSuccess) return Ok(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpPut("profile")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileResponse updateInfo)
        {
            var userName = User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _userService.UpdateUserProfileAsync(userName,updateInfo);
            if (!result.IsSuccess) return Ok(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpPost("change-password")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordRequest request)
        {
            var userName = User.Claims.FirstOrDefault(x => x.Type == "name").Value;
            var result = await _userService.ChangeUserPasswordAsync(userName, request);
            if (!result.IsSuccess) return Ok(result.Message);
            return Ok(result.ResultObj);
        }
    }
}
