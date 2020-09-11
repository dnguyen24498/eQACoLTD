using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class Cart
    {
        public Guid? UserId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }

        public AppUser AppUser { get; set; }
        public Product Product { get; set; }
    }
}
