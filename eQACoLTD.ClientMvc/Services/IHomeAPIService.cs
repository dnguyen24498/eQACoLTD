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
        Task<ApiResult<List<CategoryAndBrandsResponse>>> GetCategoryHomeAsync();
        Task<ApiResult<List<ListProductHomeResponse>>> GetRandomProductAsync();
        Task<ApiResult<List<ListProductHomeResponse>>> GetBestSellProductsAsync();
        Task<ApiResult<List<ListProductHomeResponse>>> GetNewArrivedProductsAsync();
        Task<ApiResult<List<ListProductHomeResponse>>> GetProductsTopViewdAsync();
        Task<ApiResult<List<ListProductHomeResponse>>> GetProductsTopRatedAsync();
        Task<ApiResult<List<ListProductHomeResponse>>> GetFeaturedProductsAsync();
    }
}
