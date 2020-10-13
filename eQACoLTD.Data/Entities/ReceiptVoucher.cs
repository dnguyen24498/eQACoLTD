using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class ReceiptVoucher
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public decimal Received { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string PaymentMethodId { get; set; }
        public bool IsDelete { get; set; }
        public string Description { get; set; }
        public string SupplierId { get; set; }
        public string CustomerId { get; set; }
        public DateTime DateCreated { get; set; }
        public string EmployeeId { get; set; }
        public string BranchId { get; set; }
        public string RepairVoucherId { get; set; }
        public string LiquidationVoucherId { get; set; }
        public string ReturnId { get; set; }
        public string ShippingId { get; set; }


        public Supplier Supplier { get; set; }
        public Customer Customer { get; set; }
        public Order Order { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Employee Employee { get; set; }
        public Branch Branch { get; set; }
        public RepairVoucher RepairVoucher { get; set; }
        public LiquidationVoucher LiquidationVoucher { get; set; }
        public Return Return { get; set; }
        public Shipping Shipping { get; set; }
    }
}
