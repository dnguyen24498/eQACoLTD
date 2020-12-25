namespace eQACoLTD.ViewModel.Product.ListProduct.Queries
{
    public class PromotionDetailDto
    {
        public string Id { get; set; }
        public string PromotionId { get; set; }
        public string ProductId { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
    }
}