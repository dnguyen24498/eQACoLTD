using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Customer.Queries
{
    public class CustomerDetailResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime Dob { get; set; }
        public string FullAddress { get; set; }
        public bool? Gender { get; set; }
        public string AvatarPath { get; set; }
        public bool IsDelete { get; set; }
        public string CustomerTypeId { get; set; }
        public decimal TotalDebt { get; set; }
    }
}
