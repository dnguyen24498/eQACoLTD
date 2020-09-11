using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Customer.Handlers;
using eQACoLTD.ViewModel.Customer.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eQACoLTD.Application.Customer
{
    public interface ICustomerService
    {
        Task<ApiResult<CustomerDetailResponse>> GetCustomerAsync(string customerId);
        Task<ApiResult<PagedResult<CustomerResponse>>> GetCustomersPagingAsync(int pageIndex);
        Task<ApiResult<PagedResult<CustomerHistoryResponse>>> GetCustomerHistoryPagingAsync(string customerId, int pageIndex);
        Task<ApiResult<string>> PostCustomerAsync(CustomerRequest request);
        Task DeleteCustomerAsync(string customerId);
        Task<ApiResult<CustomerDetailResponse>> PutCustomerAsync(string customerId,CustomerRequest request);
    }
}
