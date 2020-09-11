using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Entities
{
    public class Employee
    {
        public string Id { get; set; }
        public DateTime Dob { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool? Gender { get; set; }
        public string AvatarPath { get; set; }
        public bool IsDelete { get; set; }
        public Guid? UserId { get; set; }
        public string DefaultPhoneNumber { get; set; }

        public AppUser AppUser { get; set; }

        public List<Supplier> Suppliers { get; set; }
        public List<StockHistory> StockHistories { get; set; }
    }
}
