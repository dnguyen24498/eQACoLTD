using System.Collections.Generic;

namespace eQACoLTD.Data.Entities
{
    public class TransactionStatus
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<PurchaseOrder> PurchaseOrders { get; set; }
        public List<Order> Orders { get; set; }
    }
}