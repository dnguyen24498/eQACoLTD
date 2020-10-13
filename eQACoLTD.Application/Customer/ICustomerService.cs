using System.Collections.Generic;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Customer.Handlers;
using eQACoLTD.ViewModel.Customer.Queries;

namespace eQACoLTD.Application.Customer
{
    public interface ICustomerService
    {
        Task<ApiResult<PagedResult<CustomersDto>>> GetCustomersPagingAsync(int pageIndex, int pageSize);
        Task<ApiResult<CustomerDto>> GetCustomerAsync(string customerId);
        Task<ApiResult<PagedResult<CustomerHistoriesDto>>> GetCustomerHistoriesAsync(string customerId, int pageIndex, int pageSize);
        Task<ApiResult<string>> CreateCustomerAsync(CustomerForCreationDto creationDto);
        Task<ApiResult<string>> DeleteCustomerAsync(string customerId);
        Task<ApiResult<IEnumerable<CustomerDto>>> SearchCustomerAsync(string customerName);
    }
}