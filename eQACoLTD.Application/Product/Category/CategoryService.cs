using Dapper;
using eQACoLTD.Application.Extensions;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Category.Handlers;
using eQACoLTD.ViewModel.Product.Category.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.Product.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly IConfiguration _configuration;
        private readonly IStorageService _storageService;
        public CategoryService(IConfiguration configuration,IStorageService storageService)
        {
            _configuration = configuration;
            _storageService = storageService;
        }

        private async Task<CategoryResponse> CheckCategoryAsync(string idCategory)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var category = await connection.QueryFirstOrDefaultAsync<CategoryResponse>
                    ($"SELECT * FROM Categories WHERE Id='{idCategory}'");
                if (category !=null) return category;
                return null;
            }
        }

        public async Task DeleteCategoryAsync(string idCategory)
        {
            var query = $"DELETE FROM Categories where Id='{idCategory}'";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var checkCategory = await CheckCategoryAsync(idCategory);
                if (checkCategory!=null)
                {
                    await connection.ExecuteAsync(query);
                    await _storageService.DeleteFileAsync(checkCategory.ThumbnailImagePath);
                }
            }
        }

        public async Task<ApiResult<PagedResult<CategoryResponse>>> GetCategoryPagingAsync(int pageIndex)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var categories = await connection.QueryAsync<CategoryResponse>
                    (@"EXEC prGetCategoryPaging @pageNumber=@PageNumber,@rowsOfPage=@RowsOfPage",
                    new { PageNumber = pageIndex, RowsOfPage = int.Parse(_configuration["PageSize"]) });
                return new ApiSuccessResult<PagedResult<CategoryResponse>>
                    (categories.MapPage(pageIndex, int.Parse(_configuration["PageSize"]),
                    categories.Count()>0?categories.ElementAt(0).TotalRecord:0));
            }
        }

        public async Task<ApiResult<string>> PostCategoryAsync(CategoryRequest request)
        {
            var newId = Guid.NewGuid().ToString();
            string query = $"INSERT INTO Categories VALUES('{newId}',@Name,@Description)";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(query,request);
                return new ApiSuccessResult<string>(newId);
            }
        }

        public async Task<ApiResult<CategoryResponse>> PutCategoryAsync(string categoryId,CategoryRequest request)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                if (await (CheckCategoryAsync(categoryId))!=null)
                {
                    string query = $"UPDATE Categories SET Name=@Name,Description=@Description " +
                        $"WHERE Id='{categoryId}'";
                    await connection.ExecuteAsync(query,request);
                    return new ApiSuccessResult<CategoryResponse>(await CheckCategoryAsync(categoryId));
                }
            }
            return new ApiErrorResult<CategoryResponse>("Danh mục không tồn tại");
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<ApiResult<string>> PostCategoryImage(CategoryImageRequest request)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var imagePath = await this.SaveFile(request.Imagefile);
                await connection.ExecuteAsync($"UPDATE Categories SET ThumbnailImagePath='{imagePath}' " +
                    $"WHERE Id='{request.CategoryId}'");
                return new ApiSuccessResult<string>(imagePath);
            }
        }

        public async Task<ApiResult<List<CategoryAndBrandsResponse>>> GetCategoryAndBrandsAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var categoriesReturn = new List<CategoryAndBrandsResponse>();
                var categories = await connection.QueryAsync<CategoryResponse>
                    (@"SELECT * FROM Categories");
                foreach (var c in categories)
                {
                    categoriesReturn.Add(new CategoryAndBrandsResponse()
                    {
                        CategoryName = c.Name,
                        BrandsName = (List<string>)await connection.QueryAsync<string>
                        ($"SELECT DISTINCT Brands.Name FROM Brands " +
                        $"LEFT JOIN Products ON Products.BrandId = Brands.Id " +
                        $"LEFT JOIN Categories ON Categories.Id = Products.CategoryId WHERE Categories.Id = '{c.Id}'")
                    });
                }
                return new ApiSuccessResult<List<CategoryAndBrandsResponse>>(categoriesReturn);
            }
        }
    }
}
