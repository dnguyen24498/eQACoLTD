using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class Customer
    {
        public string Id { get; set; }
        public DateTime Dob { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool? Gender { get; set; }
        public bool IsDelete { get; set; }
        public Guid? AppUserId { get; set; }
        public string CustomerTypeId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public string EmployeeId { get; set; }
        
        

        public CustomerType CustomerType { get; set; }
        public Employee Employee { get; set; }
        public AppUser AppUser { get; set; }
        public List<Shipping> Shippings { get; set; }
        public List<PaymentVoucher> PaymentVouchers { get; set; }
        public List<ReceiptVoucher> ReceiptVouchers { get; set; }
        public List<CustomerPromotion> CustomerPromotions { get; set; }
        public List<Warranty> Warranties { get; set; }
        public List<Order> Orders { get; set; }
        public List<RepairVoucher> RepairVouchers { get; set; }
        public List<LiquidationVoucher> LiquidationVouchers { get; set; }
    }
}
