using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class Stock
    {
        public string ProductId { get; set; }
        public int AbleToSale { get; set; }
        public int Inventory { get; set; }

        public Product Product { get; set; }
    }
}
