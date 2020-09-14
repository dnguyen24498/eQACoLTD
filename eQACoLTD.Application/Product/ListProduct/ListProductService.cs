using Dapper;
using eQACoLTD.Application.Extensions;
using eQACoLTD.Data.Entities;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.ListProduct.Handlers;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eQACoLTD.Application.Product.ListProduct
{
    public class ListProductService : IListProductService
    {
        private readonly IConfiguration _configuration;
        private readonly IStorageService _storageService;
        public ListProductService(IConfiguration configuration,IStorageService storageService)
        {
            _configuration = configuration;
            _storageService = storageService;
        }

        public async Task DeleteProductAsync(string productId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if(await CheckProductAsync(productId) != null)
                {
                    await connection.ExecuteAsync($"UPDATE Products SET IsDelete=1 WHERE Id='{productId}'");
                }
            }
        }

        public async Task DeleteProductImageAsync(Guid imageId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var productImage = await connection.QueryFirstOrDefaultAsync<ProductImage>
                    ($"SELECT * FROM ProductImages WHERE Id='{imageId}'");
                if (productImage !=null)
                {
                    await connection.ExecuteAsync($"DELETE FROM ProductImages WHERE Id='{imageId}'");
                    await _storageService.DeleteFileAsync(productImage.ImagePath);
                }
            }
        }


        public async Task<ApiResult<ListProductDetailResponse>> GetProductAsync(string productId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var product = await connection.QueryFirstOrDefaultAsync<ListProductDetailResponse>
                    (@"EXEC prGetProductDetail @productId=@ProductId", new { ProductId = productId });
                return new ApiSuccessResult<ListProductDetailResponse>(product);
            }
        }

        public async Task<ApiResult<IEnumerable<ListProductImageResponse>>> GetProductImageAsync(string productId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var productImages = await connection.QueryAsync<ListProductImageResponse>
                    ($"SELECT * FROM ProductImages WHERE ProductId='{productId}'");
                return new ApiSuccessResult<IEnumerable<ListProductImageResponse>>(productImages);
            }
        }

        public async Task<ApiResult<PagedResult<ListProductResponse>>> GetProductPagingAsync(int pageIndex)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var listProducts = await connection.QueryAsync<ListProductResponse>
                    (@"EXEC prGetProductPaging @pageNumber=@PageNumber,@rowsOfPage=@RowsOfPage",
                    new { PageNumber = pageIndex, RowsOfPage = int.Parse(_configuration["PageSize"]) });
                return new ApiSuccessResult<PagedResult<ListProductResponse>>(listProducts.MapPage(
                    pageIndex, int.Parse(_configuration["PageSize"]),
                    listProducts.Count()>0?listProducts.ElementAt(0).TotalRecord:0));
            }
        }

        public async Task<ApiResult<List<ListProductHomeResponse>>> GetProductsTopRatedAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var products = (List<ListProductHomeResponse>)await connection.QueryAsync<ListProductHomeResponse>
                    (@"EXEC prGetProductsTopRated");
                return new ApiSuccessResult<List<ListProductHomeResponse>>(products);
            }
        }

        public async Task<ApiResult<List<ListProductHomeResponse>>> GetProductsTopViewedAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var products = (List<ListProductHomeResponse>)await connection.QueryAsync<ListProductHomeResponse>
                    (@"EXEC prGetProductsTopViewed");
                return new ApiSuccessResult<List<ListProductHomeResponse>>(products);
            }
        }

        public async Task<ApiResult<List<ListProductHomeResponse>>> GetRandomProductAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var products = (List<ListProductHomeResponse>)await connection.QueryAsync<ListProductHomeResponse>
                    (@"EXEC prGetRandomProduct");
                return new ApiSuccessResult<List<ListProductHomeResponse>>(products);
            }
        }

        public async Task<ApiResult<List<ListProductHomeResponse>>> GetBestSellProductsAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var products = (List<ListProductHomeResponse>)await connection.QueryAsync<ListProductHomeResponse>
                    (@"EXEC prGetBestSellProducts");
                return new ApiSuccessResult<List<ListProductHomeResponse>>(products);
            }
        }
        public async Task<ApiResult<List<ListProductHomeResponse>>> GetFeaturedProductsAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var products =(List<ListProductHomeResponse>) await connection.QueryAsync<ListProductHomeResponse>
                    (@"EXEC prGetFeaturedProducts");
                return new ApiSuccessResult<List<ListProductHomeResponse>>(products);
            }
        }

        public async Task<ApiResult<List<ListProductHomeResponse>>> GetNewArrivedProductsAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var products = (List<ListProductHomeResponse>)await connection.QueryAsync<ListProductHomeResponse>
                    (@"EXEC prGetNewArrivedProducts");
                return new ApiSuccessResult<List<ListProductHomeResponse>>(products);
            }
        }
        public async Task<ApiResult<string>> PostProductAsync(PostListProductRequest request)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var sequenceNumber = await connection.QueryFirstOrDefaultAsync<int>
                    ($"SELECT COUNT(*) FROM Products");
                var id=IdentifyGenerator.GenerateProductId(sequenceNumber + 1);
                var query = $"INSERT INTO Products(Id,Name,Information,CategoryId,Description,RetailPrice," +
                    $"WholesalePrices,WarrantyPeriod,BrandId) VALUES('{id}',N'{request.Name}'," +
                    $"N'{request.Information}','{request.CategoryId}',N'{request.Description}'," +
                    $"{request.RetailPrice},{request.WholesalePrices},{request.WarrantyPeriod},'{request.BrandId}')";
                await connection.ExecuteAsync(query);
                return new ApiSuccessResult<string>(id);
            }
        }

        public async Task<ApiResult<Guid>> PostProductImageAsync(string productId,ListProductImageRequest request)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var imageId = Guid.NewGuid();
                var imagePath = await this.SaveFile(request.Image,imageId);
                if (1==request.IsThumbnail)
                {
                    var query = $"UPDATE ProductImages " +
                        $"SET IsThumbnail=0 " +
                        $"FROM ProductImages LEFT JOIN Products ON Products.Id=ProductImages.ProductId " +
                        $"WHERE ProductImages.IsThumbnail=1 AND Products.Id='{productId}'";
                    await connection.ExecuteAsync(query);
                }
                var insertQuery = $"INSERT INTO ProductImages(Id,ProductId,ImagePath,FullPath,IsThumbnail) " +
                    $"VALUES('{imageId}','{productId}'," +
                    @$"'{imagePath}','{_configuration["BackendServerHost"]}/app-content/{imagePath}',{request.IsThumbnail})";
                await connection.ExecuteAsync(insertQuery);
                return new ApiSuccessResult<Guid>(imageId);
            }
        }

        private async Task<ListProductDetailResponse> CheckProductAsync(string productId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var product = await connection.QueryFirstOrDefaultAsync<ListProductDetailResponse>
                    (@"EXEC prGetProductDetail @productId=@ProductId", new { ProductId = productId });
                if (product != null) return product;
                return null;
            }
        }

        private async Task<string> SaveFile(IFormFile file,Guid guid)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{guid}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
