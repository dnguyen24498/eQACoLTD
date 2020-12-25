namespace eQACoLTD.Data.Entities
{
    public class WarrantyDetail
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string WarrantyId { get; set; }
        public int WarrantyPeriods { get; set; }
        public Product Product { get; set; }
        public Warranty Warranty { get; set; }
    }
}