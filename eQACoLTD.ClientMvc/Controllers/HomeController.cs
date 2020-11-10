using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using eQACoLTD.ClientMvc.Models;
using eQACoLTD.ClientMvc.Services;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;
using Microsoft.AspNetCore.Authorization;

namespace eQACoLTD.ClientMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeAPIService _apiService;
        
        public HomeController(IHomeAPIService apiService,ILogger<HomeController> logger)
        {
            _logger = logger;
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _apiService.GetCategoryHomeAsync();
            return View(result.ResultObj);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public async Task<IActionResult> Products(string categoryId,int page=1,int size=15)
        {
            var result = await _apiService.GetProductsByCategoryPagingAsync(categoryId, page, size);
            if (result.Code != HttpStatusCode.OK) return View(new PagedResult<ProductCardDto>());
            return View(result.ResultObj);
        }
        
        public async Task<IActionResult> Search(string categoryId, string searchValue,int page=1,int size=16)
        {
            var result = await _apiService.SearchProductsByCategory(categoryId, searchValue, page, size);
            if (result.Code != HttpStatusCode.OK) return View(nameof(Products),new PagedResult<ProductCardDto>());
            return View(nameof(Products),result.ResultObj);
        }

        public async Task<IActionResult> Filter(string categoryId, string brandId, bool order, int page = 1,
            int size = 16, decimal minimumPrice=0m, decimal maximumPrice=999999999m)
        {
            var result = await _apiService.FilterProductsByCategoryAsync(categoryId, brandId, order, minimumPrice,
                maximumPrice, page, size);
            if (result.Code != HttpStatusCode.OK) return View(nameof(Products),new PagedResult<ProductCardDto>());
            return View(nameof(Products),result.ResultObj);
        }

        [Authorize]
        public IActionResult Login()
        {
            return View(nameof(Index));
        }
    }
}
