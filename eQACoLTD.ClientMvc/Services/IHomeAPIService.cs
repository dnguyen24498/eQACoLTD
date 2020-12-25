using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Category.Queries;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eQACoLTD.ClientMvc.Services
{
    public interface IHomeAPIService
    {
        Task<ApiResult<List<CategoriesDto>>> GetCategoryHomeAsync();
        Task<ApiResult<List<ProductCardDto>>> GetRandomProductAsync();
        Task<ApiResult<List<ProductCardDto>>> GetBestSellProductsAsync();
        Task<ApiResult<List<ProductCardDto>>> GetNewArrivedProductsAsync();
        Task<ApiResult<List<ProductCardDto>>> GetProductsTopViewAsync();
        Task<ApiResult<List<ProductCardDto>>> GetProductsTopRatedAsync();
        Task<ApiResult<List<ProductCardDto>>> GetFeaturedProductsAsync();
        Task<ApiResult<PagedResult<ProductCardDto>>> GetProductsByCategoryPagingAsync(string categoryId,int pageIndex,int pageSize);

        Task<ApiResult<PagedResult<ProductCardDto>>> SearchProductsByCategory(string categoryId, string searchValue,
            int pageNumber, int pageSize);
        Task<ApiResult<PagedResult<ProductCardDto>>> FilterProductsByCategoryAsync(string categoryId, string brandId, bool order,
            decimal minimumPrice, decimal maximumPrice,int pageNumber, int pageSize);
    }
}
