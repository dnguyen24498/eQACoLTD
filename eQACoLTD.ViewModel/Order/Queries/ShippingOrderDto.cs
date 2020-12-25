namespace eQACoLTD.ViewModel.Order.Queries
{
    public class ShippingOrderDto
    {
        public string TransporterId { get; set; }
        public string Description { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public decimal Fee { get; set; }
    }
}