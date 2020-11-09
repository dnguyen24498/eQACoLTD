using System;

namespace eQACoLTD.ViewModel.Product.Payment.Queries
{
    public class ReceiptVouchersDto
    {
        public string Id { get; set; }
        public string PersonName { get; set; }
        public DateTime ReceivedDate { get; set; }
        public decimal Received { get; set; }
        public string PaymentMethodName { get; set; }
        public string BranchName { get; set; }
        public string EmployeeName { get; set; }
    }
}