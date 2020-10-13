namespace eQACoLTD.Data.Entities
{
    public class LiquidationVoucherDetail
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string LiquidationVoucherId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public LiquidationVoucher LiquidationVoucher { get; set; }
        public Product Product { get; set; }
        
    }
}