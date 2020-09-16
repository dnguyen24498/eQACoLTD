using System.Collections.Generic;
using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Other.Queries;

namespace eQACoLTD.Application.Other
{
    public interface IOtherService
    {
        Task<ApiResult<List<BrandResponse>>> GetBrandsAsync();
        Task<ApiResult<List<AllCategoryResponse>>> GetAllCategoryAsync();
    }
}