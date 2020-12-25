using System;
using System.Collections.Generic;

namespace eQACoLTD.Data.Entities
{
    public class InventoryVoucher
    {
        public string Id { get; set; }
        public DateTime InventoryDate { get; set; }
        public string EmployeeId { get; set; }
        public string WarehouseId { get; set; }
        public bool IsConfirm { get; set; }

        public Employee Employee { get; set; }
        public Warehouse Warehouse { get; set; }
        public List<InventoryVoucherDetail> InventoryVoucherDetails { get; set; }
    }
}