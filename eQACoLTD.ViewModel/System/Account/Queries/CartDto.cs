using System.Collections;
using System.Collections.Generic;

namespace eQACoLTD.ViewModel.System.Account.Queries
{
    public class CartDto
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CartDetailDto> ListProduct { get; set; }
    }
}