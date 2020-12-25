using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class PurchaseOrder
    {
        public string Id { get; set; }
        public string SupplierId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string PaymentStatusId { get; set; }
        public bool IsDelete { get; set; }
        public decimal DiscountValue { get; set; }
        public string DiscountDescription { get; set; }
        public string DiscountType { get; set; }
        public string BrandId { get; set; }
        public string TransactionStatusId { get; set; }
        public string Description { get; set; }
        public string EmployeeId { get; set; }
        public decimal TotalAmount { get; set; }

        public Supplier Supplier { get; set; }
        public Branch Branch { get; set; }
        public Employee Employee { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public List<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public List<PaymentVoucher> PaymentVouchers { get; set; }
        public List<GoodsReceivedNote> GoodsReceivedNote { get; set; }
        public List<Return> Returns { get; set; }
    }
}
