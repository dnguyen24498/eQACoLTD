using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eQACoLTD.Application.Product.ListProduct;
using eQACoLTD.ViewModel.Product.ListProduct.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IListProductService _productService;

        public ProductsController(IListProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListProducts(int pageIndex = 1, int pageSize = 15)
        {
            var result = await _productService.GetProductsPagingAsync(pageIndex, pageSize);
            if (result.Code == HttpStatusCode.NoContent) return NoContent();
            return Ok(result.ResultObj);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProducts(string productId)
        {
            var result = await _productService.GetProductAsync(productId);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductForCreationDto creationDto)
        {
            var result = await _productService.CreateProductAsync(creationDto);
            if (result.Code == HttpStatusCode.InternalServerError)
                return StatusCode(500, result.Message);
            return Ok(result.ResultObj);
        }

        [HttpPost("{productId}/images")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddImages(string productId, [FromForm] IList<IFormFile> files)
        {
            var result = await _productService.AddImageToProductAsync(productId, files);
            if (result.Code == HttpStatusCode.InternalServerError)
                return StatusCode(500, result.Message);
            if (result.Code == HttpStatusCode.NotFound)
                return NotFound(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpGet("best-selling-in-month")]
        public async Task<IActionResult> GetBestSellingInMonth()
        {
            var result = await _productService.GetBestSellingInMonth();
            if (result.Code == HttpStatusCode.NoContent) return NoContent();
            return Ok(result.ResultObj);
        }

        [HttpGet("random")]
        public async Task<IActionResult> GetRandom()
        {
            var result = await _productService.GetRandom();
            if (result.Code == HttpStatusCode.NoContent) return NoContent();
            return Ok(result.ResultObj);
        }

        [HttpGet("new-arrived")]
        public async Task<IActionResult> NewArrived()
        {
            var result = await _productService.GetNewArrived();
            if (result.Code == HttpStatusCode.NoContent) return NoContent();
            return Ok(result.ResultObj);
        }
        [HttpGet("top-view")]
        public async Task<IActionResult> TopView()
        {
            var result = await _productService.TopView();
            if (result.Code == HttpStatusCode.NoContent) return NoContent();
            return Ok(result.ResultObj);
        }
        [HttpGet("top-rate")]
        public async Task<IActionResult> TopRate()
        {
            var result = await _productService.TopRate();
            if (result.Code == HttpStatusCode.NoContent) return NoContent();
            return Ok(result.ResultObj);
        }
        [HttpGet("best-selling")]
        public async Task<IActionResult> BestSelling()
        {
            var result = await _productService.GetBestSelling();
            if (result.Code == HttpStatusCode.NoContent) return NoContent();
            return Ok(result.ResultObj);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProductByCategory(string categoryId, string searchValue,
            int pageNumber = 1, int pageSize = 15)
        {
            var result = await _productService.SearchProductsByCategory(categoryId, searchValue, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterProductByCategory(string categoryId, string brandId, int pageNumber = 1, int pageSize = 15,
            decimal minimumPrice=0m,decimal maximumPrice=999999999m)
        {
            var result = await _productService.FilterProductsByCategoryAsync(categoryId, brandId, minimumPrice,
                maximumPrice, pageNumber, pageSize);
            if (result.Code == HttpStatusCode.BadRequest) return BadRequest(result.Message);
            return Ok(result.ResultObj);
        }
    }
}
