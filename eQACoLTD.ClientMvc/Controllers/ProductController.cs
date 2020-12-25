using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eQACoLTD.ClientMvc.Services;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.ClientMvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductAPIService _apiService;

        public ProductController(IProductAPIService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> Index(string productId)
        {
            var result = await _apiService.GetProductAsync(productId);
            if (result.Code != HttpStatusCode.OK) return View(new ProductDto());
            return View(result.ResultObj);
        }
    }
}
