using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Customer.Queries;

namespace eQACoLTD.AdminMvc.Services
{
    public interface ICustomerAPIService
    {
        Task<ApiResult<PagedResult<CustomersDto>>> GetCustomersPagingAsync(int pageIndex,int pageSize);
    }
}