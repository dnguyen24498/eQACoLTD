using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string OverView { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public int Views { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal WholesalePrices { get; set; }
        public bool IsDelete { get; set; }
        public string BrandId { get; set; }
        public int Stars { get; set; }
        public int WarrantyPeriod { get; set; }
        public int MinimumQuantity { get; set; }
        public int MaximumQuantity { get; set; }

        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public List<Cart> Carts { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductEvaluation> ProductEvaluations { get; set; }
        public List<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public List<Stock> Stocks { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<PromotionDetail> PromotionDetails { get; set; }
        public List<GoodsReceivedNoteDetail> GoodsReceivedNoteDetails { get; set; }
        public List<WarrantyDetail> WarrantyDetails { get; set; }
        public List<GoodsDeliveryNoteDetail> GoodsDeliveryNoteDetails { get; set; }
        public List<ReturnDetail> ReturnDetails { get; set; }
        public List<RepairVoucherDetail> RepairVoucherDetails { get; set; }
        public List<InventoryVoucherDetail> InventoryVoucherDetails { get; set; }
        public List<LiquidationVoucherDetail> LiquidationVoucherDetails { get; set; }
    }
}
