using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal WholesalePrices { get; set; }
        public bool IsDelete { get; set; }
        public string BrandId { get; set; }
        public int StarScore { get; set; }
        public int WarrantyPeriod { get; set; }

        public Category Category { get; set; }
        public Brand Brand { get; set; }

        public List<Cart> Carts { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductReview> ProductReviews { get; set; }
        public List<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public Stock Stock { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<StockHistory> StockHistories { get; set; }
    }
}
