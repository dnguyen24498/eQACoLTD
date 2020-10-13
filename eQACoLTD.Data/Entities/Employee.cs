using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class Employee
    {
        public string Id { get; set; }
        public DateTime Dob { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool? Gender { get; set; }
        public bool IsDelete { get; set; }
        public Guid? AppuserId { get; set; }
        public string DepartmentId { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string BranchId { get; set; }

        public AppUser AppUser { get; set; }
        public Department Department { get; set; }
        public Branch Branch { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Warehouse> Warehouses { get; set; }
        public List<PurchaseOrder> PurchaseOrders { get; set; }
        public List<GoodsReceivedNote> GoodsReceivedNotes { get; set; }
        public List<PaymentVoucher> PaymentVouchers { get; set; }
        public List<Warranty> Warranties { get; set; }
        public List<Order> Orders { get; set; }
        public List<GoodsDeliveryNote> GoodsDeliveryNotes { get; set; }
        public List<ReceiptVoucher> ReceiptVouchers { get; set; }
        public List<InventoryVoucher> InventoryVouchers { get; set; }
        public List<Return> Returns { get; set; }
        public List<RepairVoucher> RepairVouchers { get; set; }
    }
}
