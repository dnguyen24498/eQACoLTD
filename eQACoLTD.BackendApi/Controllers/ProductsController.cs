using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IListProductService _listProductService;
        public ProductsController(IListProductService listProductService)
        {
            _listProductService = listProductService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductPaging(int pageIndex = 1)
        {
            var result = await _listProductService.GetProductPagingAsync(pageIndex);
            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProduct(string productId)
        {
            var result = await _listProductService.GetProductAsync(productId);
            return Ok(result);
        }
        [HttpPost("{productId}/images")]
        public async Task<IActionResult> PostProductImage(string productId,[FromForm] ListProductImageRequest request)
        {
            var result = await _listProductService.PostProductImageAsync(productId,request);
            return Ok(result);
        }
        [HttpDelete("images/{imageId}")]
        public async Task<IActionResult> DeleteProductImage(Guid imageId)
        {
            await _listProductService.DeleteProductImageAsync(imageId);
            return Ok();
        }
        [HttpGet("{productId}/images")]
        public async Task<IActionResult> GetProductImage(string productId)
        {
            var result = await _listProductService.GetProductImageAsync(productId);
            return Ok(result);
        }
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            await _listProductService.DeleteProductAsync(productId);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] PostListProductRequest request)
        {
            var result = await _listProductService.PostProductAsync(request);
            return Ok(result);
        }
        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedProducts()
        {
            var result = await _listProductService.GetFeaturedProductsAsync();
            return Ok(result);
        }
        [HttpGet("best-sell")]
        public async Task<IActionResult> GetBestSellProducts()
        {
            var result = await _listProductService.GetBestSellProductsAsync();
            return Ok(result);
        }
        [HttpGet("new-arrived")]
        public async Task<IActionResult> GetNewArrivedProducts()
        {
            var result = await _listProductService.GetNewArrivedProductsAsync();
            return Ok(result);
        }
        [HttpGet("suggestion")]
        public async Task<IActionResult> GetSuggestionProducts()
        {
            var result = await _listProductService.GetRandomProductAsync();
            return Ok(result);
        }
        [HttpGet("top-rated")]
        public async Task<IActionResult> GetTopRatedProducts()
        {
            var result = await _listProductService.GetProductsTopRatedAsync();
            return Ok(result);
        }
        [HttpGet("top-viewed")]
        public async Task<IActionResult> GetTopViewedProducts()
        {
            var result = await _listProductService.GetProductsTopViewedAsync();
            return Ok(result);
        }
    }
}
