using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Category.Queries;

namespace eQACoLTD.AdminMvc.Services
{
    public interface ICategoryService
    {
        Task<ApiResult<PagedResult<CategoryResponse>>> GetCategoryPagingAsync(int pageIndex);
    }
}