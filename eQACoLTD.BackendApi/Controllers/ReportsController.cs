using System.Threading.Tasks;
using eQACoLTD.Application.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class  ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }
        [HttpGet("debt/customers")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,Accountant")]
        public async Task<IActionResult> GetCustomersDebt(int pageIndex=1,int pageSize=15)
        {
            var result = await _reportService.GetAllCustomerDebtAsync(pageIndex,pageSize);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("debt/suppliers")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,Accountant")]
        public async Task<IActionResult> GetSuppliersDebt(int pageIndex = 1, int pageSize = 15)
        {
            var result = await _reportService.GetAllSupplierDebtAsync(pageIndex, pageSize);
            return StatusCode((int)result.Code, result);
        }
    }
}