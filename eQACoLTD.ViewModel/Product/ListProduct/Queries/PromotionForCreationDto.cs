using System;
using System.Collections.Generic;

namespace eQACoLTD.ViewModel.Product.ListProduct.Queries
{
    public class PromotionForCreationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        private DateTime fromDate;
        public DateTime FromDate { get=>fromDate;
            set
            {
                fromDate = value.ToLocalTime();
            }
        }

        private DateTime toDate;
        public DateTime ToDate { get=>toDate;
            set
            {
                toDate = value.ToLocalTime();
            }
        }
        public IEnumerable<PromotionDetailForCreationDto> Products { get; set; }
    }
}