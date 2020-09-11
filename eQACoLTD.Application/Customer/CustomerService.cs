using Dapper;
using eQACoLTD.Application.Extensions;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Customer.Handlers;
using eQACoLTD.ViewModel.Customer.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly IConfiguration _configuration;
        private readonly IStorageService _storageService;
        public CustomerService(IConfiguration configuration,IStorageService storageService)
        {
            _configuration = configuration;
            _storageService = storageService;
        }

        public async Task DeleteCustomerAsync(string customerId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                if(await CheckCustomerAsync(customerId) != null)
                {
                    await connection.ExecuteAsync($"UPDATE Customers SET IsDelete={1} WHERE Id='{customerId}'");
                }
            }
        }

        public async Task<ApiResult<CustomerDetailResponse>> GetCustomerAsync(string customerId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var customer = await connection.QueryFirstOrDefaultAsync<CustomerDetailResponse>
                    (@"EXEC prGetCustomerById @customerId=@CustomerId",
                    new { CustomerId = customerId });
                if (customer != null)
                {
                    customer.TotalDebt = await connection.QueryFirstAsync<decimal>(@"SELECT dbo.fuGetCustomerDebt(@CustomerId)",
                   new { CustomerId = customerId });
                    return new ApiSuccessResult<CustomerDetailResponse>(customer);
                }
                return new ApiErrorResult<CustomerDetailResponse>($"Khách hàng không tồn tại");
            }
        }

        public async Task<ApiResult<PagedResult<CustomerHistoryResponse>>> GetCustomerHistoryPagingAsync(string customerId, int pageIndex)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var customerHistoies = await connection.QueryAsync<CustomerHistoryResponse>
                    (@"EXEC prGetCustomerHistoryPaging @customerId=@CustomerId,@pageNumber=@PageNumber,@rowsOfPage=@RowsOfPage",
                    new {CustomerId=customerId,PageNumber=pageIndex,RowsOfPage= int.Parse(_configuration["PageSize"]) });
                return new ApiSuccessResult<PagedResult<CustomerHistoryResponse>>
                    (customerHistoies.MapPage(pageIndex, int.Parse(_configuration["PageSize"]),
                    customerHistoies.Count()>0?customerHistoies.ElementAt(0).TotalRecord:0));
            }
        }

        public async Task<ApiResult<PagedResult<CustomerResponse>>> GetCustomersPagingAsync(int pageIndex)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var customers = await connection.QueryAsync<CustomerResponse>
                    (@"EXEC prGetCustomerPaging @pageNumber=@PageNumber,@rowsOfPage=@RowsOfPage",
                    new { PageNumber = pageIndex, RowsOfPage = int.Parse(_configuration["PageSize"]) });
                return new ApiSuccessResult<PagedResult<CustomerResponse>>
                    (customers.MapPage(pageIndex, int.Parse(_configuration["PageSize"]),
                    customers.Count()>0?customers.ElementAt(0).TotalRecord:0));
            }
        }

        public async Task<ApiResult<string>> PostCustomerAsync(CustomerRequest request)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var sequenceNumber = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT COUNT(*) FROM Customers");
                var id=IdentifyGenerator.GenerateCustomerId(sequenceNumber + 1);
                var query = "INSERT INTO Customers(Id,Dob,FullName,Address," +
                    "Gender,CustomerTypeId,DefaultPhoneNumber) VALUES" +
                $"('{id}','{(request.Dob.ToString() == null ? "NULL" : request.Dob.ToString("yyyy/MM/dd"))}'," +
                $"N'{request.FullName}'," +
                $"N'{request.Address}',{request.Gender}," +
                $"'{request.CustomerTypeId}','{request.DefaultPhoneNumber}')";
                await connection.ExecuteAsync(query, request);
                return new ApiSuccessResult<string>(id);
            }
        }

        public async Task<ApiResult<CustomerDetailResponse>> PutCustomerAsync(string customerId, CustomerRequest request)
        {
            var query = $"UPDATE Customers SET Dob='{request.Dob}'," +
                $"FullName=N'{request.FullName}',Address=N'{request.Address}'," +
                $"Gender={request.Gender}," +
                $"CustomerTypeId='{request.CustomerTypeId}',DefaultPhoneNumber='{request.DefaultPhoneNumber}' " +
                $"WHERE Id='{customerId}'";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                if (await CheckCustomerAsync(customerId)!=null)
                {
                    await connection.ExecuteAsync(query, request);
                    return new ApiSuccessResult<CustomerDetailResponse>(await CheckCustomerAsync(customerId));
                }
                return new ApiErrorResult<CustomerDetailResponse>("Khách hàng không tồn tại");
            }
        }

        private async Task<CustomerDetailResponse> CheckCustomerAsync(string customerId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var customer = await connection.QueryFirstOrDefaultAsync<CustomerDetailResponse>
                    (@"EXEC prGetCustomerById @customerId=@CustomerId",
                    new { CustomerId = customerId });
                if (customer != null) return customer;
                return null;
            }
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
