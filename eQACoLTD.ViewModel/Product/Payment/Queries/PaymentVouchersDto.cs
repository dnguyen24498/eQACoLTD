using System;

namespace eQACoLTD.ViewModel.Product.Payment.Queries
{
    public class PaymentVouchersDto
    {
        public string Id { get; set; }
        public string PersonName { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Paid { get; set; }
        public string PaymentMethodName { get; set; }
        public string BranchName { get; set; }
        public string EmployeeName { get; set; }
    }
}