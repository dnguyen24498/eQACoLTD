using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eQACoLTD.AdminMvc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.AdminMvc.Controllers
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly IRoleApiClient _roleApiClient;

        public RolesController(IRoleApiClient roleApiClient)
        {
            _roleApiClient = roleApiClient;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int page=1)
        {
            var result = await _roleApiClient.GetRolesPagingAsync(page);
            if (!result.IsSuccess) return Ok(result.Message);
            return View(result.ResultObj);
        }
    }
}
