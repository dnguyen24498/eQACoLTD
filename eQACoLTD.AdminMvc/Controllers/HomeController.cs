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
using eQACoLTD.AdminMvc.Handlers;
using Microsoft.CodeAnalysis;

namespace eQACoLTD.AdminMvc.Controllers
{
    //[CustomAuthorize(Permissions = "SuperAdministrator,Accountant,Technician,Cashier,WarehouseManager,WarehouseStaff,BusinessStaff," +
    //    "Administrator,Salesman,Manager,CashManager")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();   
        }
    }
}
