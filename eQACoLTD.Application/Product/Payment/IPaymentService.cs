using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Payment.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.Product.Payment
{
    public interface IPaymentService
    {
        Task<ApiResult<string>> OrderReceiveAsync(string employeeId,string orderId, OrderPaymenForCreationDto creationDto);
        Task<ApiResult<string>> PurchaseOrderPaymentAsync(string employeeId, string purchaseOrderId, PurchaseOrderPaymentForCreationDto creationDto);
    }
}
