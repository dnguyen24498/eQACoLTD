using System.Threading.Tasks;
using eQACoLTD.AdminMvc.Services;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Category.Handlers;
using eQACoLTD.ViewModel.Product.Category.Queries;
using eQACoLTD.ViewModel.Product.ListProduct.Handlers;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;
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
        public async Task<IActionResult> Categories(int page=1)
        {
            var result = await _productService.GetCategoryPagingAsync(page);
            if (!result.IsSuccess) return View(new PagedResult<CategoryResponse>());
            else return View(result.ResultObj);
        }
        public IActionResult CreateCategory()
        {
            return View();
        }

        public async Task<IActionResult> Products(int page = 1)
        {
            var result = await _productService.GetProductPagingAsync(page);
            if (!result.IsSuccess) return View((new PagedResult<ListProductResponse>()));
            else return View(result.ResultObj);
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateProduct([FromForm] PostListProductRequest request)
        {
            var result = await _productService.PostProductAsync(request);
            return View();
        }
        
    }
}