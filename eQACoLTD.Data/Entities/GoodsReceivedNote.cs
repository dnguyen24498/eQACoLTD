using System;
using System.Collections.Generic;

namespace eQACoLTD.Data.Entities
{
    public class GoodsReceivedNote
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
        

        public PurchaseOrder PurchaseOrder { get; set; }
        public Employee Employee { get; set; }
        public StockAction StockAction { get; set; }
        public Warehouse Warehouse { get; set; }
        public RepairVoucher RepairVoucher { get; set; }
        public Return Return { get; set; }
        public List<GoodsReceivedNoteDetail> GoodsReceivedNoteDetails { get; set; }
        public StockBook StockBook { get; set; }
    }
}