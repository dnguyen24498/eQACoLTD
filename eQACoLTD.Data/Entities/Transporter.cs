using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class Transporter
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public List<ShippingOrder> ShippingOrders { get; set; }
    }
}
