using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Category.Queries;
using eQACoLTD.ViewModel.Product.ListProduct.Handlers;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;

namespace eQACoLTD.AdminMvc.Services
{
    public interface IProductAPIService
    {
        Task<ApiResult<PagedResult<ProductsDto>>> GetProductPagingAsync(int pageIndex,int pageSize);
        Task<ApiResult<string>> PostProductAsync(ProductForCreationDto forCreationDto);
    }
}