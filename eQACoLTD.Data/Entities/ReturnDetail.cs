namespace eQACoLTD.Data.Entities
{
    public class ReturnDetail
    {
        public string Id { get; set; }
        public string ReturnId { get; set; }
        public int Quantity { get; set; }
        public string ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }

        public Return Return { get; set; }
        public Product Product { get; set; }
    }
}