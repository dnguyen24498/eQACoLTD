using System.Threading.Tasks;
using eQACoLTD.AdminMvc.Services;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Category.Queries;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.AdminMvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Category(int page=1)
        {
            var result = await _productService.GetCategoryPagingAsync(page);
            if (!result.IsSuccess) return View(new PagedResult<CategoryResponse>());
            else return View(result.ResultObj);
        }
    }
}