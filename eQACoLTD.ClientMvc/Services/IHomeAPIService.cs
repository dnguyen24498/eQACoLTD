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
        Task<ApiResult<List<CategoriesHomePageDto>>> GetCategoryHomeAsync();
        Task<ApiResult<List<ProductHomePageDto>>> GetRandomProductAsync();
        Task<ApiResult<List<ProductHomePageDto>>> GetBestSellProductsAsync();
        Task<ApiResult<List<ProductHomePageDto>>> GetNewArrivedProductsAsync();
        Task<ApiResult<List<ProductHomePageDto>>> GetProductsTopViewdAsync();
        Task<ApiResult<List<ProductHomePageDto>>> GetProductsTopRatedAsync();
        Task<ApiResult<List<ProductHomePageDto>>> GetFeaturedProductsAsync();
    }
}
