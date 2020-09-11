using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.ListProduct.Handlers;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.Product.ListProduct
{
    public interface IListProductService
    {
        Task<ApiResult<PagedResult<ListProductResponse>>> GetProductPagingAsync(int pageIndex);
        Task<ApiResult<ListProductDetailResponse>> GetProductAsync(string productId);
        Task<ApiResult<Guid>> PostProductImageAsync(string productId,ListProductImageRequest request);
        Task DeleteProductImageAsync(Guid imageId);
        Task<ApiResult<IEnumerable<ListProductImageResponse>>> GetProductImageAsync(string productId);
        Task DeleteProductAsync(string productId);
        Task<ApiResult<string>> PostProductAsync(PostListProductRequest request);
        Task<ApiResult<List<ListProductHomeResponse>>> GetRandomProductAsync();
        Task<ApiResult<List<ListProductHomeResponse>>> GetBestSellProductsAsync();
        Task<ApiResult<List<ListProductHomeResponse>>> GetNewArrivedProductsAsync();
        Task<ApiResult<List<ListProductHomeResponse>>> GetProductsTopViewedAsync();
        Task<ApiResult<List<ListProductHomeResponse>>> GetProductsTopRatedAsync();
        Task<ApiResult<List<ListProductHomeResponse>>> GetFeaturedProductsAsync();
    }
}
