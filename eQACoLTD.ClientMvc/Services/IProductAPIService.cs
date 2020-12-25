using System.Threading.Tasks;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.ListProduct.Queries;

namespace eQACoLTD.ClientMvc.Services
{
    public interface IProductAPIService
    {
        Task<ApiResult<ProductDto>> GetProductAsync(string productId);
        Task<ApiResult<PromotionDto>> GetClosetPromotion();
    }
}