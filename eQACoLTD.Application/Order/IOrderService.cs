using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Order.Handlers;
using eQACoLTD.ViewModel.Order.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.Order
{
    public interface IOrderService
    {
        Task<ApiResult<PagedResult<OrdersDto>>> GetOrdersPagingAsync(string branchId,int pageIndex, int pageSize);
        Task<ApiResult<OrderDto>> GetOrderAsync(string orderId);
        Task<ApiResult<string>> CreateOrderAsync(OrderForCreationDto creationDto);
        Task<ApiResult<string>> ExportStockOrderAsync(string orderId,OrderGoodsDeliveryNoteForCreationDto creationDto);
        Task<ApiResult<PagedResult<InventoryTransactionOrdersDto>>> GetExportOrdersPagingAsync(string branchId,int pageIndex, int pageSize);
        Task<ApiResult<string>> AddOrderReceiptVoucherAsync(string orderId,OrderReceiptVoucherForCreationDto creationDto);
    }
}
