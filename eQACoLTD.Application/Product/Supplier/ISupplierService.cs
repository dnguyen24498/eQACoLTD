using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Supplier.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.Product.Supplier.Handlers;

namespace eQACoLTD.Application.Product.Supplier
{
    public interface ISupplierService
    {
        Task<ApiResult<PagedResult<SuppliersDto>>> GetSuppliersPagingAsync(int pageIndex, int pageSize);
        Task<ApiResult<SupplierDto>> GetSupplierAsync(string supplierId);
        Task<ApiResult<string>> CreateSupplierAsync(SupplierForCreationDto creationDto,string accountId);
        Task<ApiResult<string>> DeleteSupplierAsync(string supplierId);
        Task<ApiResult<PagedResult<SupplierImportHistoriesDto>>> GetSupplierImportHistoriesPagingAsync(string supplierId,int pageIndex,int pageSize);
    }
}
