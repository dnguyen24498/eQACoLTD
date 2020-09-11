using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Category.Handlers;
using eQACoLTD.ViewModel.Product.Category.Queries;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.Product.Category
{
    public interface ICategoryService
    {
        Task<ApiResult<PagedResult<CategoryResponse>>> GetCategoryPagingAsync(int pageIndex);
        Task<ApiResult<string>> PostCategoryAsync(CategoryRequest request);
        Task<ApiResult<CategoryResponse>> PutCategoryAsync(string categoryId,CategoryRequest request);
        Task DeleteCategoryAsync(string idCategory);
        Task<ApiResult<string>> PostCategoryImage(CategoryImageRequest request);
        Task<ApiResult<List<CategoryAndBrandsResponse>>> GetCategoryAndBrandsAsync();
    }
}
