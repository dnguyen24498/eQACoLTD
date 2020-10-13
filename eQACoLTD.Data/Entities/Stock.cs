using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class Stock
    {
        public string ProductId { get; set; }
        public string WarehouseId { get; set; }
        public int RealQuantity { get; set; }
        public int AbleToSale { get; set; }
        public string PlacedLocation { get; set; }

        public Product Product { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
