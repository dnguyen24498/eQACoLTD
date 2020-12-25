using eQACoLTD.ClientMvc.Common;
using eQACoLTD.ClientMvc.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eQACoLTD.ClientMvc.Components
{
    public class FeaturedViewComponent:ViewComponent
    {
        private readonly IHomeAPIService _homeService;
        public FeaturedViewComponent(IHomeAPIService homeService)
        {
            _homeService = homeService;
        }
        public async Task<IViewComponentResult> InvokeAsync(ProductTabs featuredTabs)
        {
            if (featuredTabs == ProductTabs.Featured)
            {
                var result = await _homeService.GetFeaturedProductsAsync();
                return View("Default", result.ResultObj);
            }
            else if (featuredTabs == ProductTabs.TopRate)
            {
                var result = await _homeService.GetProductsTopRatedAsync();
                return View("Default", result.ResultObj);
            }
            else if (featuredTabs == ProductTabs.TopView)
            {
                var result = await _homeService.GetProductsTopViewAsync();
                return View("Default", result.ResultObj);
            }
            return View();
        }
    }
}
