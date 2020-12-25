using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class Brand
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; }

    }
}
