using System.Collections.Generic;

namespace eQACoLTD.Data.Entities
{
    public class Branch
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Warehouse> Warehouses { get; set; }

        public List<PurchaseOrder> PurchaseOrders { get; set; }
        public List<PaymentVoucher> PaymentVouchers { get; set; }
        public List<Order> Orders { get; set; }
        public List<ReceiptVoucher> ReceiptVouchers { get; set; }
        public List<Return> Returns { get; set; }
        public List<RepairVoucher> RepairVouchers { get; set; }

    }
}