using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class Supplier
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDelete { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string EmployeeId { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }

    
        public Employee Employee { get; set; }
        public List<PurchaseOrder> PurchaseOrders { get; set; }
        public List<PaymentVoucher> PaymentVouchers { get; set; }
        public List<ReceiptVoucher> ReceiptVouchers { get; set; }
    }
}
