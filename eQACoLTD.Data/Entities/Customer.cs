using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class Customer
    {
        public string Id { get; set; }
        public DateTime Dob { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool? Gender { get; set; }
        public string AvatarPath { get; set; }
        public bool IsDelete { get; set; }
        public Guid? UserId { get; set; }
        public string CustomerTypeId { get; set; }
        public string DefaultPhoneNumber { get; set; }

        public CustomerType CustomerType { get; set; }
        public AppUser AppUser { get; set; }
        public List<Order> Orders { get; set; }
        public List<ShippingOrder> ShippingOrders { get; set; }
        public List<PaymentVoucher> PaymentVouchers { get; set; }
        public List<ReceiptVoucher> ReceiptVouchers { get; set; }
    }
}
