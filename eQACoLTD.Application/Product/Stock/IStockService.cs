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
        Task<ApiResult<PagedResult<ImportsQueueDto>>> GetImportQueuePagingAsync(string employeeId, int pageIndex, int pageSize);
        Task<ApiResult<PagedResult<ExportsQueueDto>>> GetExportQueuePagingAsync(string employeeId, int pageIndex, int pageSize);
        Task<ApiResult<string>> ExportOrderAsync(string employeeId,string orderId, ExportOrderDto orderDto);
        Task<ApiResult<string>> ImportPurchaseOrderAsync(string employeeId, string purchaseOrderId, ImportPurchaseOrderDto orderDto);
        Task<ApiResult<bool>> OrderIsExport(string orderId);
    }
}
