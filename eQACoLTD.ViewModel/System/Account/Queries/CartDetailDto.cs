namespace eQACoLTD.ViewModel.System.Account.Queries
{
    public class CartDetailDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string ImagePath { get; set; }
    }
}