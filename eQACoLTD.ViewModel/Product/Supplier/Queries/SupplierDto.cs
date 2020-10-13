﻿using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.Supplier.Queries
{
    public class SupplierDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string EmployeeName { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public decimal TotalDebt { get; set; }
    }
}
