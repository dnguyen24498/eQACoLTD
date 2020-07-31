using eQACoLTD.AdminMvc.Constants;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace eQACoLTD.AdminMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(ILogger<HomeController> logger,IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            HttpContext.Session.SetString("access_token", accessToken);
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Logout()
        {
            return SignOut("Cookie", "oidc");
        }


        //private async Task<string> GetSecret(string accessToken)
        //{
        //    var apiClient = _httpClientFactory.CreateClient();

        //    apiClient.SetBearerToken(accessToken);

        //    apiClient.BaseAddress =new System.Uri(ConstantProperties.BackendAPIEndPoint);
        //    var response = await apiClient.GetAsync("https://localhost:5001/api/secret/admin");

        //    var content = await response.Content.ReadAsStringAsync();
        //    return content;
        //}
    }
}
