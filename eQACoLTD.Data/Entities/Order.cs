using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class Order
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Note { get; set; }
        public string OrderStatusId { get; set; }
        public string PaymentStatusId { get; set; }
        public bool IsDelete { get; set; }
        public string DiscountTypeId { get; set; }
        public decimal DiscountValue { get; set; }
        public string DiscountDescription { get; set; }


        public DiscountType DiscountType { get; set; }
        public Customer Customer { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<ReceiptVoucher> ReceiptVouchers { get; set; }
        public List<ShippingOrder> ShippingOrders { get; set; }
    }
}
