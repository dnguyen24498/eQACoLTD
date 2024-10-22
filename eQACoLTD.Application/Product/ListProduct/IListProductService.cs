﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.ListProduct.Handlers;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;
using Microsoft.AspNetCore.Http;

namespace eQACoLTD.Application.Product.ListProduct
{
    public interface IListProductService
    {
        Task<ApiResult<PagedResult<ProductsDto>>> GetProductsPagingAsync(int pageNumber, int pageSize);
        Task<ApiResult<ProductDto>> GetProductAsync(string productId);
        Task<ApiResult<string>> AddImageToProductAsync(string productId, IList<IFormFile> files);
        Task<ApiResult<string>> CreateProductAsync(ProductForCreationDto creationDto);
        Task<ApiResult<IEnumerable<ProductCardDto>>> GetRandom();
        Task<ApiResult<IEnumerable<ProductCardDto>>> GetBestSellingInMonth();
        Task<ApiResult<IEnumerable<ProductCardDto>>> GetBestSelling();
        Task<ApiResult<IEnumerable<ProductCardDto>>> GetNewArrived();
        Task<ApiResult<IEnumerable<ProductCardDto>>> TopView();
        Task<ApiResult<IEnumerable<ProductCardDto>>> TopRate();
        Task<ApiResult<PagedResult<ProductCardDto>>> SearchProductsByCategory(string categoryId, string searchValue,int pageNumber,int pageSize);

        Task<ApiResult<PagedResult<ProductCardDto>>> FilterProductsByCategoryAsync(string categoryId, string brandId, bool order,
            decimal minimumPrice, decimal maximumPrice,int pageNumber, int pageSize);

        Task<ApiResult<PagedResult<PromotionsDto>>> GetPromotionsPagingAsync(int pageIndex, int pageSize);
        Task<ApiResult<PromotionDto>> GetPromotionDetail(string promotionId);
        Task<ApiResult<string>> CreatePromotionAsync(PromotionForCreationDto creationDto);
        Task<ApiResult<PromotionDto>> GetClosetPromotion();
        Task<ApiResult<string>> AddProductToPromotion(string promotionId,PromotionDetailForCreationDto creationDto);
        Task<ApiResult<string>> DeleteProductFromPromotion(string promotionId,string productId);
        Task<ApiResult<string>> DeletePromotion(string promotionId);
    }
}