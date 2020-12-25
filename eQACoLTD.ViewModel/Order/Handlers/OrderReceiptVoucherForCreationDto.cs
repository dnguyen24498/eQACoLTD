using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Order.Handlers
{
    public class OrderReceiptVoucherForCreationDto
    {
        public decimal Received { get; set; }
        public string PaymentMethodId { get; set; }
        public string EmployeeId { get; set; }
        public string Description { get; set; }
        public string BranchId { get; set; }
        public DateTime ReceivedDate { get; set; }
    }
}
