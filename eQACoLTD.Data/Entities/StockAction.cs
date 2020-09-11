using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class StockAction
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<StockHistory> StockHistories { get; set; }
    }
}
