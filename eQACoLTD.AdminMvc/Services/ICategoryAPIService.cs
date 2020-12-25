using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Category.Queries;

namespace eQACoLTD.AdminMvc.Services
{
    public interface ICategoryAPIService
    {
        Task<ApiResult<PagedResult<CategoriesDto>>> GetCategoryPagingAsync(int pageIndex,int pageSize);
    }
}