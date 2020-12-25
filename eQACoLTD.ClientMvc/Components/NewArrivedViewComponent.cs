using eQACoLTD.ClientMvc.Services;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace eQACoLTD.ClientMvc.Components
{
    public class NewArrivedViewComponent:ViewComponent
    {
        private readonly IHomeAPIService _homeService;
        public NewArrivedViewComponent(IHomeAPIService homeService)
        {
            _homeService = homeService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _homeService.GetNewArrivedProductsAsync();
            if (result.Code!=HttpStatusCode.OK) return View("Default", new List<ProductCardDto>());
            return View("Default", result.ResultObj);
        }

    }
}
