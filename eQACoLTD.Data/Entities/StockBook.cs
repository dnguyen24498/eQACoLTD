using System;

namespace eQACoLTD.Data.Entities
{
    public class StockBook
    {
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string GoodsReceivedNoteId { get; set; }
        public string GoodsDeliveryNoteId { get; set; }
        public int ImportQuantity { get; set; }
        public decimal ImportUnitPrice { get; set; }
        public int ExportQuantity { get; set; }
        public decimal ExportUnitPrice { get; set; }
        public int InventoryQuantity { get; set; }
        public decimal InventoryUnitPrice { get; set; }

        public GoodsReceivedNote GoodsReceivedNote { get; set; }
        public GoodsDeliveryNote GoodsDeliveryNote { get; set; }
    }
}