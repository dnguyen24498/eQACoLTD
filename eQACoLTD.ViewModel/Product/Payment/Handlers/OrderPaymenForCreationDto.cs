﻿using eQACoLTD.ViewModel.Product.Stock.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.ViewModel.Product.Payment.Handlers
{
    public class OrderPaymenForCreationDto
    {
        public decimal Received { get; set; }
        public string PaymentMethodId { get; set; }
        public string Description { get; set; }
        public DateTime ReceivedDate { get; set; }

        public IEnumerable<ImportPurchaseOrderDetailsDto> ListSubOrder { get; set; }
    }
}
