namespace eQACoLTD.Data.Entities
{
    public class RepairVoucherDetail
    {
        public string Id { get; set; }
        public string RepairVoucherId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string RepairContent { get; set; }
        public string ProductName { get; set; }
        public bool IsFixed { get; set; }

        public RepairVoucher RepairVoucher { get; set; }
        public Product Product { get; set; }
        
    }
}