namespace eQACoLTD.ViewModel.Product.Stock.Queries
{
    public class ProductInStock
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int AbleToSale { get; set; }
        public int RealQuantity { get; set; }
        public string BranchName { get; set; }
        public string WarehouseName { get; set; }
    }
}