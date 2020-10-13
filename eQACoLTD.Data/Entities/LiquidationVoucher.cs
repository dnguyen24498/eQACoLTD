using System;
using System.Collections.Generic;

namespace eQACoLTD.Data.Entities
{
    public class LiquidationVoucher
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Description { get; set; }
        public DateTime LiquidationDate { get; set; }
        public string WarehouseId { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string PhoneNumber { get; set; }

        public Customer Customer { get; set; }
        public Warehouse Warehouse { get; set; }
        public List<GoodsDeliveryNote> GoodsDeliveryNotes { get; set; }
        public List<ReceiptVoucher> ReceiptVouchers { get; set; }
        public List<Shipping> Shippings { get; set; }
        public List<LiquidationVoucherDetail> LiquidationVoucherDetails { get; set; }
        
    }
}