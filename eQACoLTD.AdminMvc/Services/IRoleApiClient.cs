using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Role.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eQACoLTD.AdminMvc.Services
{
    public interface IRoleApiClient
    {
        Task<ApiResult<PagedResult<RoleResponse>>> GetRolesPagingAsync(int page);
    }
}
