using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Supplier.Handlers;
using eQACoLTD.ViewModel.Product.Supplier.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.Product.Supplier
{
    public interface ISupplierService
    {
        Task<ApiResult<PagedResult<SupplierResponse>>> GetSupplierPagingAsync(int pageIndex);
        Task<ApiResult<SupplierDetailResponse>> GetSupplierAsync(string supplierId);
        Task<ApiResult<PagedResult<SupplierGoodsReceiptHistory>>> GetSupplierGoodsReceiptHistoryPagingAsync(string supplierId,int pageIndex);
        Task<ApiResult<string>> PostSupplierAsync(SupplierRequest request);
        Task DeleteSupplierAsync(string supplierId);
    }
}
