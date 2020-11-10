using eQACoLTD.Data.DBContext;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.PurchaseOrder.Queries;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using eQACoLTD.Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using eQACoLTD.Application.Configurations;
using eQACoLTD.ViewModel.Product.PurchaseOrder.Handlers;
using eQACoLTD.Application.Common;
using eQACoLTD.Data.Entities;

namespace eQACoLTD.Application.Product.PurchaseOrder
{
    public class PurchaseOrderSerivce : IPurchaseOrderService
    {
        private readonly AppIdentityDbContext _context;
        public PurchaseOrderSerivce(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<string>> CreatePurchaseOrderAsync(string accountId, PurchaseOrderForCreationDto creationDto)
        {
            try
            {
                var checkEmployee = await _context.Employees.Where(x => x.AppuserId.ToString() == accountId)
                    .SingleOrDefaultAsync();
                if (checkEmployee == null) return new ApiResult<string>(HttpStatusCode.NotFound,$"Lỗi tài khoản đăng nhập");
                var sequenceNumber = await _context.PurchaseOrders.CountAsync();
                var purchaseOrderId = IdentifyGenerator.GeneratePurchaseOrderId(sequenceNumber + 1);
                var purchaseOrder = ObjectMapper.Mapper.Map<eQACoLTD.Data.Entities.PurchaseOrder>(creationDto);
                purchaseOrder.Id = purchaseOrderId;
                purchaseOrder.TransactionStatusId = GlobalProperties.InventoryTransactionId;
                purchaseOrder.PaymentStatusId = GlobalProperties.UnpaidPaymentId;
                purchaseOrder.DateCreated = DateTime.Now;
                purchaseOrder.EmployeeId = checkEmployee.Id;
                purchaseOrder.BrandId = checkEmployee.BranchId;
                purchaseOrder.PurchaseOrderDetails = new List<Data.Entities.PurchaseOrderDetail>();
                decimal totalAmount = 0;
                foreach (var p in creationDto.ListProduct)
                {
                    var purchaseOrderDetails = ObjectMapper.Mapper.Map<PurchaseOrderDetail>(p);
                    purchaseOrderDetails.Id = Guid.NewGuid().ToString("D");
                    purchaseOrderDetails.PurchaseOrderId = purchaseOrderId;
                    purchaseOrder.PurchaseOrderDetails.Add(purchaseOrderDetails);
                    totalAmount += p.UnitPrice * p.Quantity;
                }
                if (string.IsNullOrEmpty(creationDto.DiscountType))
                {
                    purchaseOrder.DiscountType = "$";
                    purchaseOrder.DiscountValue = 0;
                }
                purchaseOrder.TotalAmount = totalAmount - (creationDto.DiscountType == "$" ? creationDto.DiscountValue
                    : (totalAmount * creationDto.DiscountValue) / 100);
                await _context.PurchaseOrders.AddAsync(purchaseOrder);
                await _context.SaveChangesAsync();
                return new ApiResult<string>(HttpStatusCode.OK) { ResultObj = purchaseOrderId,Message = "Tạo mới phiếu nhập hàng thành công"};
            }
            catch
            {
                return new ApiResult<string>(HttpStatusCode.InternalServerError){Message = "Có lỗi khi tạo phiếu nhập hàng"};
            }
        }

        public async Task<ApiResult<PurchaseOrderDto>> GetPurchaseOrderAsync(string purchaseOrderId)
        {
            var restAmount = await (from pm in _context.PaymentVouchers
                                    where pm.PurchaseOrderId == purchaseOrderId select pm.Paid).SumAsync();
            var subOrders = await (from pod in _context.PurchaseOrderDetails
                                   join p in _context.Products on pod.ProductId equals p.Id
                                   where pod.PurchaseOrderId == purchaseOrderId
                                   select new PurchaseOrderDetailsDto() { 
                                    CostName=pod.CostName,
                                    ProductId=p.Id,
                                    ProductName=p.Name,
                                    Quantity=pod.Quantity,
                                    UnitPrice=pod.UnitPrice
                                   }).ToListAsync();
            var purchaseOrder = await (from po in _context.PurchaseOrders
                                       join s in _context.Suppliers on po.SupplierId equals s.Id
                                       join b in _context.Branches on po.BrandId equals b.Id
                                       join e in _context.Employees on po.EmployeeId equals e.Id
                                       join ts in _context.TransactionStatuses on po.TransactionStatusId equals ts.Id
                                       join ps in _context.PaymentStatuses on po.PaymentStatusId equals ps.Id
                                       where po.Id == purchaseOrderId 
                                       select new PurchaseOrderDto()
                                       {
                                           Id = po.Id,
                                           BranchName = b.Name,
                                           DateCreated = po.DateCreated,
                                           DeliveryDate = po.DeliveryDate,
                                           Description = po.Description,
                                           DiscountDescription = po.DiscountDescription,
                                           DiscountType = po.DiscountType,
                                           DiscountValue = po.DiscountValue,
                                           EmployeeName = e.Name,
                                           PaymentStatusName = ps.Name,
                                           SupplierName = s.Name,
                                           TransactionStatusName = ts.Name,
                                           TotalAmount = po.TotalAmount,
                                           RestAmount = po.TotalAmount - restAmount,
                                           ListProduct = subOrders
                                       }).SingleOrDefaultAsync();
            return new ApiResult<PurchaseOrderDto>(HttpStatusCode.OK, purchaseOrder);
        }

        public async Task<ApiResult<PagedResult<PurchaseOrdersDto>>> GetPurchaseOrderPagingAsync(int pageIndex, int pageSize)
        {
            var purchaseOrders = await (from po in _context.PurchaseOrders
                                        join s in _context.Suppliers on po.SupplierId equals s.Id
                                        join e in _context.Employees on po.EmployeeId equals e.Id
                                        join ts in _context.TransactionStatuses on po.TransactionStatusId equals ts.Id
                                        join ps in _context.PaymentStatuses on po.PaymentStatusId equals ps.Id
                                        select new PurchaseOrdersDto()
                                        {
                                            DateCreated = po.DateCreated,
                                            Id = po.Id,
                                            EmployeeName = e.Name,
                                            PaymentStatusName = ps.Name,
                                            SupplierName = s.Name,
                                            TransactionStatusName = ts.Name
                                        }).GetPagedAsync(pageIndex, pageSize);
            return new ApiResult<PagedResult<PurchaseOrdersDto>>(HttpStatusCode.OK, purchaseOrders);
        }
    }
}
