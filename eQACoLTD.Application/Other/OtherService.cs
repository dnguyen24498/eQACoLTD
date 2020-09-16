using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Other.Queries;
using eQACoLTD.ViewModel.Product.Category.Queries;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NotImplementedException = System.NotImplementedException;

namespace eQACoLTD.Application.Other
{
    public class OtherService:IOtherService
    {
        private readonly IConfiguration _configuration;

        public OtherService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<ApiResult<List<BrandResponse>>> GetBrandsAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var results=await  connection.QueryAsync<BrandResponse>
                    ("SELECT Id,Name FROM Brands");
                return new ApiSuccessResult<List<BrandResponse>>(results.ToList());
            }
        }

        public async Task<ApiResult<List<AllCategoryResponse>>> GetAllCategoryAsync()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var results = await connection.QueryAsync<AllCategoryResponse>
                    ("SELECT Id,Name FROM Categories");
                return new ApiSuccessResult<List<AllCategoryResponse>>(results.ToList());
            }
        }
    }
}