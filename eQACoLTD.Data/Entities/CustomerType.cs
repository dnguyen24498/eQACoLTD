using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class CustomerType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Customer> Customers { get; set; }
    }
}
