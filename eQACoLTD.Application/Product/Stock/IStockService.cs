using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Stock.Handlers;
using eQACoLTD.ViewModel.Product.Stock.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.Product.Stock
{
    public interface IStockService
    {
        Task<ApiResult<PagedResult<ImportsQueueDto>>> GetImportQueuePagingAsync(int pageIndex, int pageSize);
        Task<ApiResult<PagedResult<ExportsQueueDto>>> GetExportQueuePagingAsync(int pageIndex, int pageSize);
        Task<ApiResult<string>> ExportOrderAsync(string accountId,string orderId, ExportOrderDto orderDto);
        Task<ApiResult<string>> ImportPurchaseOrderAsync(string accountId, string purchaseOrderId, ImportPurchaseOrderDto orderDto);
        Task<ApiResult<bool>> OrderIsExport(string orderId);
        Task<ApiResult<ExportOrderHistoriesDto>> GetExportOrderHistory(string orderId);
        Task<ApiResult<ImportPurchaseOrderHistoriesDto>> GetImportPurchaseOrderHistory(string purchaseOrderId);
        Task<ApiResult<PagedResult<ProductInStock>>> GetProductsInStockPagingAsync(int pageIndex, int pageSize, string accountId);
        Task<ApiResult<bool>> PurchaseOrderIsImport(string purchaseOrderId);
    }
}
