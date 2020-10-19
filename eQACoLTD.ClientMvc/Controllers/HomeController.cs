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

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public async Task<IActionResult> Products(string categoryId,int page=1,int size=16)
        {
            var result = await _apiService.GetProductsByCategoryPagingAsync(categoryId, page, size);
            if (result.Code != HttpStatusCode.OK) return View(new PagedResult<ProductCardDto>());
            return View(result.ResultObj);
        }

        [Authorize]
        public IActionResult Login()
        {
            return View(nameof(Index));
        }
    }
}
