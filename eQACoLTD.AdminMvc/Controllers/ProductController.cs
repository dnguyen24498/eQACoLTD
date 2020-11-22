using System.Net;
using System.Threading.Tasks;
using eQACoLTD.AdminMvc.Services;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Category.Handlers;
using eQACoLTD.ViewModel.Product.Category.Queries;
using eQACoLTD.ViewModel.Product.ListProduct.Handlers;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;
using eQACoLTD.ViewModel.Product.Stock.Queries;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.AdminMvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductAPIService _productService;

        public ProductController(IProductAPIService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int page = 1,int size=15)
        {
            var result = await _productService.GetProductPagingAsync(page,size);
            if (result.Code == HttpStatusCode.Forbidden) return View("403");
            if (result.Code!=System.Net.HttpStatusCode.OK) return View((new PagedResult<ProductsDto>()));
            else return View(result.ResultObj);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductForCreationDto forCreationDto)
        {
            var result = await _productService.PostProductAsync(forCreationDto);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Stock(int page = 1, int size = 15)
        {
            var result = await _productService.GetProductsInStockPagingAsync(page, size);
            if (result.Code == HttpStatusCode.Forbidden) return View("403");
            if (result.Code != HttpStatusCode.OK) return View(new PagedResult<ProductInStock>());
            return View(result.ResultObj);
        }

    }
}