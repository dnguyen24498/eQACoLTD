using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eQACoLTD.Application.Product.Category;
using eQACoLTD.ViewModel.Product.Category.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eQACoLTD.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories(int pageIndex = 1, int pageSize = 15)
        {
            var result = await _categoryService.GetCategoriesPagingAsync(pageIndex, pageSize);
            if (result.Code == HttpStatusCode.NoContent)
                return NoContent();
            return Ok(result.ResultObj);
        }
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategory(string categoryId)
        {
            var result = await _categoryService.GetCategoryAsync(categoryId);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            return Ok(result.ResultObj);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetCategoriesAndBrands()
        {
            var result = await _categoryService.GetCategoriesForHomePageAsync();
            if (result.Code == HttpStatusCode.NoContent)
                return NoContent();
            return Ok(result.ResultObj);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryForCreationDto categoryDto)
        {
            var result = await _categoryService.CreateCategoryAsync(categoryDto);
            if (result.Code == HttpStatusCode.BadRequest)
                return BadRequest(result.Message);
            if (result.Code == HttpStatusCode.InternalServerError)
                return StatusCode(500, result.Message);
            return Ok(result.ResultObj);
        }
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(string categoryId)
        {
            var result = await _categoryService.DeleteCategoryAsync(categoryId);
            if (result.Code == HttpStatusCode.NotFound)
                return NotFound(result.Message);
            return Ok(result.ResultObj);
        }

        [HttpGet("{categoryId}/products")]
        public async Task<IActionResult> GetProductsInCategory(string categoryId,int pageIndex=1,int pageSize=15)
        {
            var result = await _categoryService.GetProductsByCategoryPagingAsync(categoryId, pageIndex, pageSize);
            if (result.Code == HttpStatusCode.NotFound) return NotFound(result.Message);
            return Ok(result.ResultObj);
        }
    }
}
