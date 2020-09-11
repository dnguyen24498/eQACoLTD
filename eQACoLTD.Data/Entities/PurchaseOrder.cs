using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class PurchaseOrder
    {
        public string Id { get; set; }
        public string Note { get; set; }
        public string SupplierId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string OrderStatusId { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string PaymentStatusId { get; set; }
        public bool IsDelete { get; set; }
        public string DiscountTypeId { get; set; }
        public decimal DiscountValue { get; set; }
        public string DiscountDescription { get; set; }

        public DiscountType DiscountType { get; set; }
        public Supplier Supplier { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public List<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public List<PaymentVoucher> PaymentVouchers { get; set; }
    }
}
