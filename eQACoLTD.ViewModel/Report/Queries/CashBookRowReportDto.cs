using System;
using System.Collections.Generic;

namespace eQACoLTD.ViewModel.Report.Queries
{
    public class CashBookRowReportDto
    {
        public DateTime RecordDate { get; set; }
        public DateTime DateCreated { get; set; }
        public string Id { get; set; }
        public string OriginalDocumentId { get; set; }
        public string TargetPerson { get; set; }
        public string PaymentMethodName { get; set; }
        public decimal Received { get; set; }
        public decimal Paid { get; set; }
    }
}