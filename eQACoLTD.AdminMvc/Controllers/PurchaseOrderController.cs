using System.Net;
using System.Threading.Tasks;
using eQACoLTD.AdminMvc.Services;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.PurchaseOrder.Queries;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.AdminMvc.Controllers
{
    public class PurchaseOrderController : Controller
    {
        private readonly IPurchaseOrderAPIService _apiService;

        public PurchaseOrderController(IPurchaseOrderAPIService apiService)
        {
            _apiService = apiService;
        }
        // GET
        public async Task<IActionResult> Index(int page=1,int size=15)
        {
            var result = await _apiService.GetPurchaseOrderPagingAsync(page, size);
            if (result.Code != HttpStatusCode.OK) return View(new PagedResult<PurchaseOrdersDto>());
            return View(result.ResultObj);
        }
    }
}