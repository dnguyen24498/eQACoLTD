using System;

namespace eQACoLTD.ViewModel.Product.Payment.Handlers
{
    public class PaymentVoucherForCreationDto
    {
        public decimal Paid { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethodId { get; set; }
        public string Description { get; set; }
        public string CustomerId { get; set; }
        public string SupplierId { get; set; }
    }
}