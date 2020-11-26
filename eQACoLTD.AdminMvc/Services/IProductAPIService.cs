using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Category.Queries;
using eQACoLTD.ViewModel.Product.ListProduct.Handlers;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;
using eQACoLTD.ViewModel.Product.Stock.Queries;

namespace eQACoLTD.AdminMvc.Services
{
    public interface IProductAPIService
    {
        Task<ApiResult<PagedResult<ProductsDto>>> GetProductPagingAsync(int pageIndex,int pageSize);
        Task<ApiResult<string>> PostProductAsync(ProductForCreationDto forCreationDto);
        Task<ApiResult<PagedResult<ProductInStock>>> GetProductsInStockPagingAsync(int pageIndex, int pageSize);
        Task<ApiResult<PagedResult<PromotionsDto>>> GetPromotionsPagingAsync(int pageIndex, int pageSize);
        Task<ApiResult<PagedResult<GoodsDeliveryNotesDto>>>
            GetGoodsDeliveryNotePagingAsync(int pageIndex, int pageSize);
        Task<ApiResult<GoodsDeliveryNoteDto>> GetGoodsDeliveryNote(string goodsDeliveryNoteId);
    }
}