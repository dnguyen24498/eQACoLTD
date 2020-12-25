using System;
using System.Collections.Generic;

namespace eQACoLTD.Data.Entities
{
    public class GoodsDeliveryNote
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public DateTime ExportDate { get; set; }
        public string EmployeeId { get; set; }
        public string Description { get; set; }
        public string StockActionId { get; set; }
        public string WarehouseId { get; set; }
        public string ReturnId { get; set; }
        public string RepairVoucherId { get; set; }
        public string LiquidationVoucherId { get; set; }

        public Order Order { get; set; }
        public StockAction StockAction { get; set; }
        public Warehouse Warehouse { get; set; }
        public Return Return { get; set; }
        public RepairVoucher RepairVoucher { get; set; }
        public LiquidationVoucher LiquidationVoucher { get; set; }
        public Employee Employee { get; set; }

        public List<GoodsDeliveryNoteDetail> GoodsDeliveryNoteDetails { get; set; }
    }
}