namespace eQACoLTD.Data.Entities
{
    public class InventoryVoucherDetail
    {
        public string Id { get; set; }
        public string InventoryVoucherId { get; set; }
        public string ProductId { get; set; }
        public int SystemQuantity { get; set; }
        public int RealQuantity { get; set; }
        public int BadQuantity { get; set; }
        public int NormalQuantity { get; set; }
        public int ExpiredQuantity { get; set; }

        public InventoryVoucher InventoryVoucher { get; set; }
        public Product Product { get; set; }
    }
}