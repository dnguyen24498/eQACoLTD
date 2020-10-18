using System.Collections;
using System.Collections.Generic;

namespace eQACoLTD.ViewModel.System.Account.Queries
{
    public class CartDto
    {
        public string CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public IEnumerable<CartDetailDto> ListProduct { get; set; }
    }
}