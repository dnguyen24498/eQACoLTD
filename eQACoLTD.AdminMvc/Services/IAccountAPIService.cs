using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eQACoLTD.AdminMvc.Services
{
    public interface IAccountAPIService
    {
        public Task<ApiResult<PagedResult<AccountResponse>>> GetAccountsPagingAsync(int pageIndex);
        public Task<ApiResult<AccountDetailResponse>> GetAccountDetailAsync(Guid userId);
    }
}
