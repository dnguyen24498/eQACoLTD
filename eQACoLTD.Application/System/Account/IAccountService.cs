using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Handlers;
using eQACoLTD.ViewModel.System.Account.Queries;
using eQACoLTD.ViewModel.System.User.Queries;
using System.Threading.Tasks;

namespace eQACoLTD.Application.System.User
{
    public interface IAccountService
    {
        Task<ApiResult<AccountProfileResponse>> GetAccountProfileAsync(string userName);
        Task<ApiResult<string>> ChangeAccountPasswordAsync(string userName,ChangeAccountPasswordRequest request);
        Task<ApiResult<AccountProfileResponse>> UpdateAccountProfileAsync(string userName, AccountProfileResponse request);
    }
}
