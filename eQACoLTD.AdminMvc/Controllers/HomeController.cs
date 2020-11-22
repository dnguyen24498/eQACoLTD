using System.Linq;
using System.Net;
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
using eQACoLTD.AdminMvc.Services;
using Microsoft.CodeAnalysis;

namespace eQACoLTD.AdminMvc.Controllers
{
    //[CustomAuthorize(Permissions = "SuperAdministrator,Accountant,Technician,Cashier,WarehouseManager,WarehouseStaff,BusinessStaff," +
    //    "Administrator,Salesman,Manager,CashManager")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IReportService _reportService;

        public HomeController(IReportService reportService)
        {
            _reportService = reportService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _reportService.GetOverviewReport();
            if (result.Code == HttpStatusCode.Forbidden) return View("403");
            return View(result.ResultObj);   
        }
    }
}
