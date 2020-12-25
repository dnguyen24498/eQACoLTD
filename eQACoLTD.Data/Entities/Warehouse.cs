using System.Collections.Generic;

namespace eQACoLTD.Data.Entities
{
    public class Warehouse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EmployeeId { get; set; }
        public string BranchId { get; set; }

        public Employee Employee { get; set; }
        public Branch Branch { get; set; }
        public List<Stock> Stocks { get; set; }
        public List<GoodsReceivedNote> GoodsReceivedNotes { get; set; }
        public List<GoodsDeliveryNote> GoodsDeliveryNotes { get; set; }
        public List<InventoryVoucher> InventoryVouchers { get; set; }
        public List<LiquidationVoucher> LiquidationVouchers { get; set; }
    }
}