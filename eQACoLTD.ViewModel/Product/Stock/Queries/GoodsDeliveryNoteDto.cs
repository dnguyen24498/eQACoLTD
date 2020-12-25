using System;
using System.Collections.Generic;

namespace eQACoLTD.ViewModel.Product.Stock.Queries
{
    public class GoodsDeliveryNoteDto
    {
        public string Id { get; set; }
        public DateTime ExportDate { get; set; }
        public string OrderId { get; set; }
        public string Employee { get; set; }
        public string Description { get; set; }
        public string StockAction { get; set; }
        public string Warehouse { get; set; }
        public string Branch { get; set; }

        public List<GoodsDeliveryNoteDetailsDto> Products { get; set; }
    }
}