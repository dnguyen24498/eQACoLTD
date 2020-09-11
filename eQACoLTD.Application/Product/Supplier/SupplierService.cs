using Dapper;
using eQACoLTD.Application.Extensions;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Supplier.Handlers;
using eQACoLTD.ViewModel.Product.Supplier.Queries;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.Product.Supplier
{
    public class SupplierService : ISupplierService
    {
        private readonly IConfiguration _configuration;
        public SupplierService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteSupplierAsync(string supplierId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                if(await CheckSupplierAsync(supplierId) != null)
                {
                    await connection.ExecuteAsync($"UPDATE Suppliers SET IsDelete=1 WHERE Id='{supplierId}'");
                }
            }
        }

        public async Task<ApiResult<SupplierDetailResponse>> GetSupplierAsync(string supplierId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var supplier = await connection.QueryFirstOrDefaultAsync<SupplierDetailResponse>
                    (@"EXEC prGetSupplierById @supplierId=@SupplierId", new { SupplierId = supplierId });
                if (supplier != null)
                {
                    var totalDebt = await connection.QueryFirstAsync<decimal>(@"SELECT dbo.fuGetSupplierDebt(@SupplierId)",
                        new { SupplierId = supplier.Id });
                    supplier.TotalDebt = Math.Abs(totalDebt);

                    return new ApiSuccessResult<SupplierDetailResponse>(supplier);
                }
                return new ApiErrorResult<SupplierDetailResponse>($"Nhà cung cấp không tồn tại");
            }
        }

        public async Task<ApiResult<PagedResult<SupplierGoodsReceiptHistory>>> GetSupplierGoodsReceiptHistoryPagingAsync
            (string supplierId, int pageIndex)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var supplierHistories = await connection.QueryAsync<SupplierGoodsReceiptHistory>
                    (@"EXEC prGetSupplierGoodsReceiptHistories @supplierId=@SupplierId,@pageNumber=@PageNumber,@rowsOfPage=@RowsOfPage",
                    new { SupplierId = supplierId, PageNumber = pageIndex, RowsOfPage = int.Parse(_configuration["PageSize"]) });
                return new ApiSuccessResult<PagedResult<SupplierGoodsReceiptHistory>>
                    (supplierHistories.MapPage(pageIndex, int.Parse(_configuration["PageSize"]),
                    supplierHistories.Count()>0?supplierHistories.ElementAt(0).TotalRecord:0));
            }
        }
        public async Task<ApiResult<PagedResult<SupplierResponse>>> GetSupplierPagingAsync(int pageIndex)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var suppliers = await connection.QueryAsync<SupplierResponse>
                    (@"EXEC prGetSupplierPaging @pageNumber=@PageNumber,@rowsOfPage=@RowsOfPage",
                    new { PageNumber = pageIndex, RowsOfPage = int.Parse(_configuration["PageSize"]) });
                return new ApiSuccessResult<PagedResult<SupplierResponse>>(suppliers.MapPage(pageIndex,
                    int.Parse(_configuration["PageSize"]),
                    suppliers.Count()>0?suppliers.ElementAt(0).TotalRecord:0));
            }
        }

        public async Task<ApiResult<string>> PostSupplierAsync(SupplierRequest request)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var sequenceNumber = await connection.QueryFirstOrDefaultAsync<int>
                    ($"SELECT COUNT(*) FROM Suppliers");
                var newId = IdentifyGenerator.GenerateSupplierId(sequenceNumber + 1);
                var query = $"INSERT INTO Suppliers(Id,Name,Address,PhoneNumber," +
                    $"Email,Fax,EmployeeId,Website,Description) VALUES(" +
                $"'{newId}'," +
                $"N'{request.Name}',N'{request.Address}'," +
                $"'{request.PhoneNumber}','{request.Email}','{request.Fax}'," +
                $"{(string.IsNullOrEmpty(request.EmployeeId) ? "NULL" : string.Format($"'{request.EmployeeId}'"))}," +
                $"'{request.Website}','{request.Description}')";
                await connection.ExecuteAsync(query);
                return new ApiSuccessResult<string>(newId);
            }
        }

        private async Task<SupplierDetailResponse> CheckSupplierAsync(string supplierId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var supplier = await connection.QueryFirstOrDefaultAsync<SupplierDetailResponse>
                    (@"EXEC prGetSupplierById @supplierId=@SupplierId", new { SupplierId = supplierId });
                if (supplier != null) return supplier;
                return null;
            }
        }
    }
}
