using System;
using System.Collections.Generic;

namespace eQACoLTD.Data.Entities
{
    public class Return
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string BranchId { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsImport { get; set; }
        public string Description { get; set; }
        public string PurchaseOrderId { get; set; }
        public string EmployeeId { get; set; }

        public Order Order { get; set; }
        public Employee Employee { get; set; }
        public Branch Branch { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public GoodsReceivedNote GoodsReceivedNote { get; set; }
        public List<PaymentVoucher> PaymentVouchers { get; set; }
        public List<GoodsDeliveryNote> GoodsDeliveryNotes { get; set; }
        public List<ReceiptVoucher> ReceiptVouchers { get; set; }
        public List<ReturnDetail> ReturnDetails { get; set; }
    }
}