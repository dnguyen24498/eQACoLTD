using System;
using System.Collections.Generic;

namespace eQACoLTD.Data.Entities
{
    public class Warranty
    {
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string OrderId { get; set; }
        public string Description { get; set; }
        public string CustomerId { get; set; }
        public string EmployeeId { get; set; }

        public Order Order { get; set; }
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
        public List<WarrantyDetail> WarrantyDetails { get; set; }
        
    }
}