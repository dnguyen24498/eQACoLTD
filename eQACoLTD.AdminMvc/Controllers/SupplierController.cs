using System.Net;
using eQACoLTD.AdminMvc.Services;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Supplier.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eQACoLTD.AdminMvc.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierAPIService _supplierAPIService;
        public SupplierController(ISupplierAPIService supplierAPIService)
        {
            _supplierAPIService = supplierAPIService;
        }
        public async Task<IActionResult> Index(int page=1,int size=15)
        {
            var result = await _supplierAPIService.GetSupplierPagingAsync(page, size);
            if (result.Code == HttpStatusCode.Forbidden) return View("403");
            if (result.Code != System.Net.HttpStatusCode.OK) return View(new PagedResult<SuppliersDto>());
            return View(result.ResultObj);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}