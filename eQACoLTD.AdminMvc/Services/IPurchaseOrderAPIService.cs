using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.PurchaseOrder.Queries;

namespace eQACoLTD.AdminMvc.Services
{
    public interface IPurchaseOrderAPIService
    {
        Task<ApiResult<PagedResult<PurchaseOrdersDto>>> GetPurchaseOrderPagingAsync(int pageIndex, int pageSize);
    }
}