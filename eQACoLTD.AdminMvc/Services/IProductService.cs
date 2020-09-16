using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Category.Queries;
using eQACoLTD.ViewModel.Product.ListProduct.Handlers;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;

namespace eQACoLTD.AdminMvc.Services
{
    public interface IProductService
    {
        Task<ApiResult<PagedResult<CategoryResponse>>> GetCategoryPagingAsync(int pageIndex);
        Task<ApiResult<PagedResult<ListProductResponse>>> GetProductPagingAsync(int pageIndex);
        Task<ApiResult<string>> PostProductAsync(PostListProductRequest request);
    }
}