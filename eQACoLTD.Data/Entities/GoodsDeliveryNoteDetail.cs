namespace eQACoLTD.Data.Entities
{
    public class GoodsDeliveryNoteDetail
    {
        public string Id { get; set; }
        public string GoodsDeliveryNoteId { get; set; }
        public string ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public GoodsDeliveryNote GoodsDeliveryNote { get; set; }
        public Product Product { get; set; }
        
    }
}