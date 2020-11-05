using System;
using System.Collections.Generic;

namespace eQACoLTD.Data.Entities
{
    public class RepairVoucher
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string BranchId { get; set; }
        public string EmployeeId { get; set; }
        public bool IsDelete { get; set; }

        public Customer Customer { get; set; }
        public Branch Branch { get; set; }
        public Employee Employee { get; set; }
        
        public List<GoodsReceivedNote> GoodsReceivedNote { get; set; }
        public List<GoodsDeliveryNote> GoodsDeliveryNotes { get; set; }
        public List<ReceiptVoucher> ReceiptVouchers { get; set; }
        public List<RepairVoucherDetail> RepairVoucherDetails { get; set; }
    }
}