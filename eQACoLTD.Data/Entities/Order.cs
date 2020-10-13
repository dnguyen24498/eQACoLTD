using System;
using System.Collections.Generic;

namespace eQACoLTD.Data.Entities
{
    public class Order
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string TransactionStatusId { get; set; }
        public string PaymentStatusId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public string EmployeeId { get; set; }
        public decimal DiscountValue { get; set; }
        public string DiscountDescription { get; set; }
        public string DiscountType { get; set; }
        public string BranchId { get; set; }
        public decimal TotalAmount { get; set; }

        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public Branch Branch { get; set; }
        public Warranty Warranty { get; set; }

        public List<GoodsDeliveryNote> GoodsDeliveryNotes { get; set; }
        public List<ReceiptVoucher> ReceiptVouchers { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Return> Returns { get; set; }
        public List<Shipping> Shippings { get; set; }
    }
}