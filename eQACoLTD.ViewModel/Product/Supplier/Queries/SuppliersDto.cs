using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.Supplier.Queries
{
    public class SuppliersDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmployeeName { get; set; }
        public decimal TotalDebt { get; set; }
    }
}
