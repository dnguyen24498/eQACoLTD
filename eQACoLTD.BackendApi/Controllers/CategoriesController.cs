using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> GetCategoryPaging(int pageIndex=1)
        {
            var result = await _categoryService.GetCategoryPagingAsync(pageIndex);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> PostCategory([FromBody] CategoryRequest request)
        {
            var result = await _categoryService.PostCategoryAsync(request);
            return Ok(result);
        }
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(string categoryId)
        {
            await _categoryService.DeleteCategoryAsync(categoryId);
            return Ok();
        }
        [HttpPut("{categoryId}")]
        public async Task<IActionResult> PutCategory(string categoryId,[FromBody] CategoryRequest request)
        {
            var result = await _categoryService.PutCategoryAsync(categoryId,request);
            return Ok(result);
        }
        [HttpPost("image")]
        public async Task<IActionResult> PostThumbnailImage([FromForm] CategoryImageRequest request)
        {
            var result = await _categoryService.PostCategoryImage(request);
            return Ok(result);
        }
        [HttpGet("home")]
        public async Task<IActionResult> GetCategoryHome()
        {
            var result = await _categoryService.GetCategoryAndBrandsAsync();
            return Ok(result);
        }
    }
}
