namespace eQACoLTD.ViewModel.Product.ListProduct.Queries
{
    public class ProductCardDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public decimal RetailPrice { get; set; }
        public int Stars { get; set; }
        public string BrandName { get; set; }
        public int Views { get; set; }
        public string ImagePath { get; set; }
        public int AbleToSale { get; set; }
    }
}