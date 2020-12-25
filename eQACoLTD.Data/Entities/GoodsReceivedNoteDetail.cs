namespace eQACoLTD.Data.Entities
{
    public class GoodsReceivedNoteDetail
    {
        public string Id { get; set; }
        public string GoodsReceivedNoteId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public GoodsReceivedNote GoodsReceivedNote { get; set; }
        public Product Product { get; set; }
    }
}