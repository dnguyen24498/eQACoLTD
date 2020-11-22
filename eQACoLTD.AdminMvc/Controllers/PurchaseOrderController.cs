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
            if (result.Code == HttpStatusCode.Forbidden) return View("403");
            if (result.Code != HttpStatusCode.OK) return View(new PagedResult<PurchaseOrdersDto>());
            return View(result.ResultObj);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Detail(string purchaseOrderId)
        {
            var result = await _apiService.GetPurchaseOrderAsync(purchaseOrderId);
            if (result.Code == HttpStatusCode.Forbidden) return View("403");
            if (result.Code != HttpStatusCode.OK) return View(new PurchaseOrderDto());
            return View(result.ResultObj);
        }
    }
}