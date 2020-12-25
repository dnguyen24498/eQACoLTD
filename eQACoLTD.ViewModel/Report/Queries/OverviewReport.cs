namespace eQACoLTD.ViewModel.Report.Queries
{
    public class OverviewReport
    {
        public int WaitingOrder { get; set; }
        public int UnfinishedOrder { get; set; }        
        public int UnfinishedPurchaseOrder { get; set; }
        public int WaitingForExport { get; set; }
        public int WaitingForImport { get; set; }
        public int TotalInventory { get; set; }
    }
}