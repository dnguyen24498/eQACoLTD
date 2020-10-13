using eQACoLTD.ClientMvc.Services;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eQACoLTD.ClientMvc.Components
{
    public class BestSellViewComponent:ViewComponent
    {
        private readonly IHomeAPIService _homeService;
        public BestSellViewComponent(IHomeAPIService homeService)
        {
            _homeService = homeService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _homeService.GetBestSellProductsAsync();
            if (result == null || !result.IsSuccess) return View("Default", new List<ProductHomePageDto>());
            return View("Default", result.ResultObj);
        }
    }
}
