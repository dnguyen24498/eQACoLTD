using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using eQACoLTD.ClientMvc.Services;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.ClientMvc.Components
{
    public class PromotionViewComponent:ViewComponent
    {
        private readonly IProductAPIService _productApiService;

        public PromotionViewComponent(IProductAPIService productApiService)
        {
            _productApiService = productApiService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _productApiService.GetClosetPromotion();
            if (result.Code!=HttpStatusCode.OK) return View("Default", new PromotionDto());
            return View("Default",result.ResultObj);
        }
    }
}