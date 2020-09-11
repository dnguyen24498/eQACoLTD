using System.Linq;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Threading.Tasks;
using eQACoLTD.AdminMvc.Global;
using Microsoft.CodeAnalysis;

namespace eQACoLTD.AdminMvc.Controllers
{
    [CustomAuthorize(Permissions = "Administrator,Receptionist,Cashier,WarehouseStaff")]
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var test = User.Claims.ToList();
            var roles = User.Claims.ToList().Where(x=>x.ValueType=="role").FirstOrDefault()?.Value;
            return View();   
        }
    }
}
