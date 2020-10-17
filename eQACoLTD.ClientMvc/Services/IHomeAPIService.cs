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
        Task<ApiResult<List<ProductCardDto>>> GetProductsTopViewdAsync();
        Task<ApiResult<List<ProductCardDto>>> GetProductsTopRatedAsync();
        Task<ApiResult<List<ProductCardDto>>> GetFeaturedProductsAsync();
    }
}
