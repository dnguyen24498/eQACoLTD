using System;

namespace eQACoLTD.Data.Entities
{
    public class GoodReceivedNote
    {
        public string Id { get; set; }
        public string PurchaseOrderId { get; set; }
        public DateTime ImportDate { get; set; }
        public string EmployeeId { get; set; }
        public string Description { get; set; }
        public string StockActionId { get; set; }
        public string WarehouseId { get; set; }
        public string RepairVoucherId { get; set; }
        public string PlacedLocation { get; set; }
        public string ReturnId { get; set; }
    }
}