namespace eQACoLTD.Data.Entities
{
    public class PromotionDetail
    {
        public string Id { get; set; }
        public string PromotionId { get; set; }
        public string ProductId { get; set; }

        public Promotion Promotion { get; set; }
        public Product Product { get; set; }
    }
}