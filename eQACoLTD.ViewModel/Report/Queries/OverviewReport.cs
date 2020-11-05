namespace eQACoLTD.ViewModel.Report.Queries
{
    public class OverviewReport
    {
        public int WaitingOrder { get; set; }
        public int UnfinishedOrder { get; set; }
        public int UnfinishedPurchaseOrder { get; set; }
        public decimal TotalCustomerDebt { get; set; }
        public decimal TotalSupplierDebt { get; set; }
        public int TotalInventory { get; set; }
    }
}