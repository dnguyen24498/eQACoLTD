using eQACoLTD.ViewModel.Customer.Handlers;
using eQACoLTD.ViewModel.Product.ListProduct.Handlers;
using eQACoLTD.ViewModel.System.Employee.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Application.Extensions
{
    public static class IdentifyGenerator
    {
        public static string GenerateCustomerId(int sequenceNumber)
        {
            string id = string.Format("CUS{0}", sequenceNumber.ToString().PadLeft(4, '0'));
            return id;
        }

        public static string GenerateEmployeeId(int sequenceNumber)
        {
            string id = string.Format("EPN{0}", sequenceNumber.ToString().PadLeft(4, '0'));
            return id;
        }

        public static string GenerateProductId(int sequenceNumber)
        {
            string id = string.Format("PRN{0}", sequenceNumber.ToString().PadLeft(4, '0'));
            return id;
        }
        public static string GenerateSupplierId(int sequenceNumber)
        {
            string id = string.Format("SUN{0}", sequenceNumber.ToString().PadLeft(4, '0'));
            return id;
        }
        public static string GenerateOrderId(int sequenceNumber)
        {
            string id = string.Format("SRN{0}", sequenceNumber.ToString().PadLeft(4, '0'));
            return id;
        }
        public static string GenerateGoodsDeliveryNoteId(int sequenceNumber)
        {
            string id = string.Format("GDN{0}", sequenceNumber.ToString().PadLeft(4, '0'));
            return id;
        }
        public static string GenerateReceiptVoucherId(int sequenceNumber)
        {
            string id = string.Format("RVN{0}", sequenceNumber.ToString().PadLeft(4, '0'));
            return id;
        }
        public static string GeneratePurchaseOrderId(int sequenceNumber)
        {
            string id = string.Format("PON{0}", sequenceNumber.ToString().PadLeft(4, '0'));
            return id;
        }
        public static string GenerateGoodsReceivedNoteId(int sequenceNumber)
        {
            string id = string.Format("GRN{0}", sequenceNumber.ToString().PadLeft(4, '0'));
            return id;
        }
        public static string GeneratePaymentVoucherId(int sequenceNumber)
        {
            string id = string.Format("PVN{0}", sequenceNumber.ToString().PadLeft(4, '0'));
            return id;
        }
    }
}
