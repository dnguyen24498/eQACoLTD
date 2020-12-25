using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eQACoLTD.Application.Product.ListProduct;
using eQACoLTD.ViewModel.Product.ListProduct.Handlers;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;
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
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetListProducts(int pageIndex = 1, int pageSize = 15)
        {
            var result = await _productService.GetProductsPagingAsync(pageIndex, pageSize);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProducts(string productId)
        {
            var result = await _productService.GetProductAsync(productId);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,WarehouseStaff,WarehouseManager")]
        public async Task<IActionResult> CreateProduct(ProductForCreationDto creationDto)
        {
            var result = await _productService.CreateProductAsync(creationDto);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost("{productId}/images")]
        [Consumes("multipart/form-data")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,WarehouseStaff,WarehouseManager")]
        public async Task<IActionResult> AddImages(string productId, [FromForm] IList<IFormFile> files)
        {
            var result = await _productService.AddImageToProductAsync(productId, files);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("best-selling-in-month")]
        public async Task<IActionResult> GetBestSellingInMonth()
        {
            var result = await _productService.GetBestSellingInMonth();
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("random")]
        public async Task<IActionResult> GetRandom()
        {
            var result = await _productService.GetRandom();
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("new-arrived")]
        public async Task<IActionResult> NewArrived()
        {
            var result = await _productService.GetNewArrived();
            return StatusCode((int)result.Code, result);
        }
        [HttpGet("top-view")]
        public async Task<IActionResult> TopView()
        {
            var result = await _productService.TopView();
            return StatusCode((int)result.Code, result);
        }
        [HttpGet("top-rate")]
        public async Task<IActionResult> TopRate()
        {
            var result = await _productService.TopRate();
            return StatusCode((int)result.Code, result);
        }
        [HttpGet("best-selling")]
        public async Task<IActionResult> BestSelling()
        {
            var result = await _productService.GetBestSelling();
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProductByCategory(string categoryId, string searchValue,
            int pageNumber = 1, int pageSize = 15)
        {
            var result = await _productService.SearchProductsByCategory(categoryId, searchValue, pageNumber, pageSize);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterProductByCategory(string categoryId, string brandId,bool order=true, int pageNumber = 1, int pageSize = 15,
            decimal minimumPrice=0m,decimal maximumPrice=999999999m)
        {
            var result = await _productService.FilterProductsByCategoryAsync(categoryId, brandId, order, minimumPrice,
                maximumPrice, pageNumber, pageSize);
            return StatusCode((int)result.Code, result);
        }
        [HttpGet("promotions")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,BusinessStaff")]
        public async Task<IActionResult> GetPromotions(int pageIndex = 1, int pageSize = 15)
        {
            var result = await _productService.GetPromotionsPagingAsync(pageIndex, pageSize);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("promotions/{promotionId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,BusinessStaff")]
        public async Task<IActionResult> GetPromotionDetail(string promotionId)
        {
            var result = await _productService.GetPromotionDetail(promotionId);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost("promotions")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,BusinessStaff")]
        public async Task<IActionResult> CreatePromotion(PromotionForCreationDto creationDto)
        {
            var result = await _productService.CreatePromotionAsync(creationDto);
            return StatusCode((int)result.Code, result);
        }

        [HttpGet("closet-promotions")]
        public async Task<IActionResult> GetClosetPromotion()
        {
            var result = await _productService.GetClosetPromotion();
            return StatusCode((int)result.Code, result);
        }

        [HttpPost("promotions/{promotionId}/promotion-details")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,BusinessStaff")]
        public async Task<IActionResult> AddProductToPromotion(string promotionId,PromotionDetailForCreationDto creationDto)
        {
            var result = await _productService.AddProductToPromotion(promotionId,creationDto);
            return StatusCode((int)result.Code, result);
        }

        [HttpDelete("promotions/{promotionId}/products/{productId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,BusinessStaff")]
        public async Task<IActionResult> RemoveProductFromPromotion(string promotionId, string productId)
        {
            var result = await _productService.DeleteProductFromPromotion(promotionId, productId);
            return StatusCode((int)result.Code, result);
        }

        [HttpDelete("promotions/{promotionId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "SuperAdministrator,BusinessStaff")]
        public async Task<IActionResult> DeletePromotion(string promotionId)
        {
            var result = await _productService.DeletePromotion(promotionId);
            return StatusCode((int)result.Code, result);
        }
    }
}
