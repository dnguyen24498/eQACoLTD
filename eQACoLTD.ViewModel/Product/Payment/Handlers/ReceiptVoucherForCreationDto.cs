using System;

namespace eQACoLTD.ViewModel.Product.Payment.Handlers
{
    public class ReceiptVoucherForCreationDto
    {
        public decimal Received { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string PaymentMethodId { get; set; }
        public string Description { get; set; }
        public string SupplierId { get; set; }
        public string CustomerId { get; set; }
    }
}