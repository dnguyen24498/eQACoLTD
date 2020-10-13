using eQACoLTD.Data.DBContext;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Stock.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using eQACoLTD.Application.Common;
using eQACoLTD.Application.Extensions;
using System.Net;
using eQACoLTD.ViewModel.Product.Stock.Handlers;
using AutoMapper;
using eQACoLTD.Application.Configurations;
using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace eQACoLTD.Application.Product.Stock
{
    public class StockService : IStockService
    {
        private readonly AppIdentityDbContext _context;
        public StockService(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<string>> ExportOrderAsync(string employeeId,string orderId, ExportOrderDto orderDto)
        {
            try
            {
                var checkOrder = await _context.Orders.FindAsync(orderId);
                if (checkOrder == null) return new ApiResult<string>(HttpStatusCode.NotFound, ($"Không tìm thấy đơn hàng có mã: {orderId}"));
                var checkEmployee = await _context.Employees.FindAsync(employeeId);
                if (checkEmployee == null) return new ApiResult<string>(HttpStatusCode.NotFound);
                if (checkOrder.BranchId == checkEmployee.BranchId)
                {
                    var goodsDeliveryNote = ObjectMapper.Mapper.Map<GoodsDeliveryNote>(orderDto);
                    var sequencyNumber = await _context.GoodsDeliveryNotes.CountAsync();
                    var goodsDeliveryNoteId = IdentifyGenerator.GenerateGoodsDeliveryNoteId(sequencyNumber + 1);
                    goodsDeliveryNote.Id = goodsDeliveryNoteId;
                    goodsDeliveryNote.OrderId = orderId;
                    var goodsDeliveryNoteDetails = await (from od in _context.OrderDetails
                                                          where od.OrderId == checkOrder.Id
                                                          select new GoodsDeliveryNoteDetail()
                                                          {
                                                              Id = Guid.NewGuid().ToString("D"),
                                                              GoodsDeliveryNoteId = goodsDeliveryNoteId,
                                                              ProductId = od.ProductId,
                                                              Quantity = od.Quantity,
                                                              UnitPrice = od.UnitPrice
                                                          }).ToListAsync();
                    foreach (var g in goodsDeliveryNoteDetails)
                    {
                        var prod = await _context.Stocks.Where(x => x.ProductId == g.ProductId && x.WarehouseId == orderDto.WarehouseId).
                            SingleOrDefaultAsync();
                        if (prod == null)
                        {
                            checkOrder.TransactionStatusId = GlobalProperties.CancelTransactionId;
                            await _context.SaveChangesAsync();
                            return new ApiResult<string>(HttpStatusCode.NotFound, $"Sản phẩm không có trong kho");
                        }
                        prod.RealQuantity -= g.Quantity;
                        prod.AbleToSale -= g.Quantity;
                        checkOrder.TransactionStatusId = GlobalProperties.TradingTransactionId;
                    }
                    goodsDeliveryNote.GoodsDeliveryNoteDetails = goodsDeliveryNoteDetails;
                    await _context.GoodsDeliveryNotes.AddAsync(goodsDeliveryNote);
                    await _context.SaveChangesAsync();
                    return new ApiResult<string>(HttpStatusCode.OK) { ResultObj = goodsDeliveryNoteId };
                }
                return new ApiResult<string>(HttpStatusCode.Forbidden,($"Tài khoản hiện tại không được phép tạo phiếu xuất kho cho chi nhánh này"));
            }
            catch
            {
                return new ApiResult<string>(HttpStatusCode.InternalServerError, "Có lỗi khi tạo phiếu xuất kho");
            }
        }

        public async Task<ApiResult<PagedResult<ExportsQueueDto>>> GetExportQueuePagingAsync(string employeeId, int pageIndex, int pageSize)
        {
            var checkEmployee = await _context.Employees.FindAsync(employeeId);
            if (checkEmployee == null) return new ApiResult<PagedResult<ExportsQueueDto>>(HttpStatusCode.NotFound);
            var orders = await (from o in _context.Orders
                                join e in _context.Employees on o.EmployeeId equals e.Id
                                join ts in _context.TransactionStatuses on o.TransactionStatusId equals ts.Id
                                join b in _context.Branches on o.BranchId equals b.Id
                                where o.TransactionStatusId == GlobalProperties.InventoryTransactionId && o.BranchId == checkEmployee.BranchId
                                select new ExportsQueueDto()
                                {
                                    BranchId = checkEmployee.BranchId,
                                    DateCreated = o.DateCreated,
                                    EmployeeName = e.Name,
                                    Description=o.Description,
                                    OrderId = o.Id,
                                    TransactionStatusName = ts.Name,
                                    BranchName = b.Name
                                }).GetPagedAsync(pageIndex, pageSize);
            return new ApiResult<PagedResult<ExportsQueueDto>>(HttpStatusCode.OK, orders);
        }

        public async Task<ApiResult<PagedResult<ImportsQueueDto>>> GetImportQueuePagingAsync(string employeeId, int pageIndex, int pageSize)
        {
            var checkEmployee = await _context.Employees.FindAsync(employeeId);
            if (checkEmployee == null) return new ApiResult<PagedResult<ImportsQueueDto>>(HttpStatusCode.NotFound);
            var orders = await (from po in _context.PurchaseOrders
                         join e in _context.Employees on po.EmployeeId equals e.Id
                         join ts in _context.TransactionStatuses on po.TransactionStatusId equals ts.Id
                         join b in _context.Branches on po.BrandId equals b.Id
                         where po.BrandId == checkEmployee.BranchId && po.TransactionStatusId == GlobalProperties.InventoryTransactionId
                         select new ImportsQueueDto()
                         {
                             BranchId = checkEmployee.BranchId,
                             Description = po.Description,
                             EmployeeName = e.Name,
                             ImportDate = po.DateCreated,
                             PurchaseOrderId = po.Id,
                             BranchName=b.Name,
                             TransactionStatusName=ts.Name
                         }).GetPagedAsync(pageIndex,pageSize);
            return new ApiResult<PagedResult<ImportsQueueDto>>(HttpStatusCode.OK, orders);
        }

        public async Task<ApiResult<string>> ImportPurchaseOrderAsync(string employeeId, string purchaseOrderId, ImportPurchaseOrderDto orderDto)
        {
            var checkPurchaseOrder = await _context.PurchaseOrders.FindAsync(purchaseOrderId);
            if (checkPurchaseOrder == null) return new ApiResult<string>(HttpStatusCode.NotFound);
            var checkEmployee = await _context.Employees.FindAsync(employeeId);
            if (checkEmployee == null) return new ApiResult<string>(HttpStatusCode.NotFound);
            if (checkEmployee.BranchId == checkPurchaseOrder.BrandId)
            {
                var sequencyNumber = await _context.GoodReceivedNotes.CountAsync();
                var goodsReceivedId = IdentifyGenerator.GenerateGoodsReceivedNoteId(sequencyNumber + 1);
                var goodsReceivedNote = ObjectMapper.Mapper.Map<GoodsReceivedNote>(orderDto);
                var purchaseOrderDetails = await (from pod in _context.PurchaseOrderDetails
                                                  where pod.PurchaseOrderId == purchaseOrderId
                                                  select new GoodsReceivedNoteDetail()
                                                  {
                                                      Id = Guid.NewGuid().ToString("D"),
                                                      GoodsReceivedNoteId = goodsReceivedId,
                                                      ProductId = pod.ProductId,
                                                      Quantity = pod.Quantity,
                                                      UnitPrice = pod.UnitPrice
                                                  }).ToListAsync();
                goodsReceivedNote.Id = goodsReceivedId;
                goodsReceivedNote.EmployeeId = checkEmployee.Id;
                goodsReceivedNote.GoodsReceivedNoteDetails = purchaseOrderDetails;
                goodsReceivedNote.PurchaseOrderId = purchaseOrderId;
                foreach (var p in goodsReceivedNote.GoodsReceivedNoteDetails)
                {
                    p.Id = Guid.NewGuid().ToString("D");
                    p.GoodsReceivedNoteId = goodsReceivedId;
                    var productInStock = await _context.Stocks.Where(x => x.ProductId == p.ProductId 
                        && x.WarehouseId == orderDto.WarehouseId).SingleOrDefaultAsync();
                    if (productInStock == null)
                    {
                        await _context.Stocks.AddAsync(new Data.Entities.Stock()
                        {
                            AbleToSale=p.Quantity,
                            ProductId=p.ProductId,
                            RealQuantity=p.Quantity,
                            WarehouseId=orderDto.WarehouseId
                        });
                    }
                    else
                    {
                        productInStock.RealQuantity += p.Quantity;
                        productInStock.AbleToSale += p.Quantity;
                    }
                }
                await _context.GoodReceivedNotes.AddAsync(goodsReceivedNote);
                checkPurchaseOrder.TransactionStatusId = GlobalProperties.TradingTransactionId;
                await _context.SaveChangesAsync();
                return new ApiResult<string>(HttpStatusCode.OK) { ResultObj=goodsReceivedId };
            }
            return new ApiResult<string>(HttpStatusCode.BadRequest,$"Tài khoản hiện tại không được phép tạo phiếu nhập cho chi nhánh này");
        }
    }
}
