using System.Collections.Generic;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Category.Handlers;
using eQACoLTD.ViewModel.Product.Category.Queries;

namespace eQACoLTD.Application.Product.Category
{
    public interface ICategoryService
    {
        Task<ApiResult<PagedResult<CategoriesDto>>> GetCategoriesPagingAsync(int pageIndex,int pageSize);
        Task<ApiResult<CategoryDto>> GetCategoryAsync(string categoryId);
        Task<ApiResult<IEnumerable<CategoryDto>>> GetCategoriesForHomePageAsync();
        Task<ApiResult<string>> CreateCategoryAsync(CategoryForCreationDto categoryDto);
        Task<ApiResult<string>> DeleteCategoryAysnc(string categoryId);
    }
}