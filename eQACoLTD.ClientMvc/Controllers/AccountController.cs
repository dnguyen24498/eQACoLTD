using System.Net;
using System.Threading.Tasks;
using eQACoLTD.ClientMvc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.ClientMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountAPIService _apiService;

        public AccountController(IAccountAPIService apiService)
        {
            _apiService = apiService;
        }
        [Authorize]
        public async Task<IActionResult> Cart()
        {
            var result = await _apiService.GetCart();
            return View(result.ResultObj);
        }
    }
}