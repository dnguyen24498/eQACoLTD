using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.PurchaseOrder.Handlers;
using eQACoLTD.ViewModel.Product.PurchaseOrder.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.Product.PurchaseOrder
{
    public interface IPurchaseOrderService
    {
        Task<ApiResult<PagedResult<PurchaseOrdersDto>>> GetPurchaseOrderPagingAsync(int pageIndex, int pageSize);
        Task<ApiResult<PurchaseOrderDto>> GetPurchaseOrderAsync(string purchaseOrderId);
        Task<ApiResult<string>> CreatePurchaseOrderAsync(string accountId, PurchaseOrderForCreationDto creationDto);
    }
}
