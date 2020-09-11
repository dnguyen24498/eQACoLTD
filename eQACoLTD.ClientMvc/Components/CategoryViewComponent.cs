using eQACoLTD.ClientMvc.Services;
using eQACoLTD.ViewModel.Product.Category.Queries;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eQACoLTD.ClientMvc.Components
{
    public class CategoryViewComponent:ViewComponent
    {
        private readonly IHomeAPIService _commonService;
        public CategoryViewComponent(IHomeAPIService commonService)
        {
            _commonService = commonService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _commonService.GetCategoryHomeAsync();
            if (result.IsSuccess) return View("Default", result.ResultObj);
            return View("Default",new List<CategoryAndBrandsResponse>());
        }
    }
}
