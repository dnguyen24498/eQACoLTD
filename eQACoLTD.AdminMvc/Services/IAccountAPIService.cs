using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.Product.ListProduct.Handlers;

namespace eQACoLTD.AdminMvc.Services
{
    public interface IAccountAPIService
    {
        public Task<ApiResult<PagedResult<AccountsDto>>> GetAccountsPagingAsync(int pageIndex,int pageSize);
        public Task<ApiResult<AccountDto>> GetAccountDetailAsync(Guid userId);
        Task<ApiResult<EmployeeInfo>> GetCurrentAccountInfo();

    }
}
