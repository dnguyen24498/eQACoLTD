namespace eQACoLTD.ViewModel.Order.Handlers
{
    public class AcceptOrderDto
    {
        public string Description { get; set; }
        public decimal DiscountValue { get; set; }
        public string DiscountDescription { get; set; }
        public string DiscountType { get; set; }
    }
}