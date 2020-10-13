namespace eQACoLTD.Data.Entities
{
    public class CustomerPromotion
    {
        public string CustomerId { get; set; }
        public string PromotionId { get; set; }
        public decimal DiscountValue { get; set; }
        public string DiscountType { get; set; }
        public string Code { get; set; }

        public Customer Customer { get; set; }
        public Promotion Promotion { get; set; }
    }
}