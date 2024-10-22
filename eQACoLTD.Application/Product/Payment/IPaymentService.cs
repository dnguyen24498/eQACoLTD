﻿using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Payment.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.Product.Payment.Queries;

namespace eQACoLTD.Application.Product.Payment
{
    public interface IPaymentService
    {
        Task<ApiResult<string>> OrderReceiveAsync(string accountId,string orderId, OrderPaymenForCreationDto creationDto);
        Task<ApiResult<string>> PurchaseOrderPaymentAsync(string accountId, string purchaseOrderId, PurchaseOrderPaymentForCreationDto creationDto);
        Task<ApiResult<bool>> IsPaidOrder(string orderId);
        Task<ApiResult<bool>> IsPaidPurchaseOrder(string purchaseOrderId);
        Task<ApiResult<IEnumerable<OrderPaymentsDto>>> GetOrderPaymentHistory(string orderId);
        Task<ApiResult<IEnumerable<PurchaseOrderPaymentsDto>>> GetPurchaseOrderPaymentHistory(string purchaseOrderId);
        Task<ApiResult<string>> CreatePaymentVoucherAsync(PaymentVoucherForCreationDto creationDto,string accountId);
        Task<ApiResult<string>> CreateReceiptVoucherAsync(ReceiptVoucherForCreationDto creationDto, string accountId);
        Task<ApiResult<PagedResult<PaymentVouchersDto>>> GetPaymentVouchersPagingAsync(int pageIndex, int pageSize, string accountId);

        Task<ApiResult<PagedResult<ReceiptVouchersDto>>> GetReceiptVoucherPagingAsync(int pageIndex, int pageSize,
            string accountId);
    }
}
