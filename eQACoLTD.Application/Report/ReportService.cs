using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eQACoLTD.Data.DBContext;
using System.Linq;
using System.Net;
using Dapper;
using eQACoLTD.Application.Common;
using eQACoLTD.Application.Extensions;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Customer.Queries;
using eQACoLTD.ViewModel.Product.Supplier.Queries;
using eQACoLTD.ViewModel.Report.Queries;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using NotImplementedException = System.NotImplementedException;

namespace eQACoLTD.Application.Report
{
    public class ReportService:IReportService
    {
        private readonly AppIdentityDbContext _context;
        private readonly IConfiguration _configuration;
        public ReportService(AppIdentityDbContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<ApiResult<ReportCustomerSupplierDebtDto>> GetAllCustomerDebtAsync(int pageIndex,int pageSize)
        {
            var customers = from c in _context.Customers
                join employee in _context.Employees on c.EmployeeId equals employee.Id
                    into EmployeeGroup
                from e in EmployeeGroup.DefaultIfEmpty()
                join appuser in _context.AppUsers on c.AppUserId equals appuser.Id
                    into AppUserGroup
                from au in AppUserGroup.DefaultIfEmpty()
                join ct in _context.CustomerTypes on c.CustomerTypeId equals ct.Id
                let customerDebt = (_context.Orders.Where(x => x.CustomerId == c.Id &&
                                                               x.TransactionStatusId !=
                                                               GlobalProperties.CancelTransactionId
                                                               && x.TransactionStatusId !=
                                                               GlobalProperties.WaitingTransactionId)
                                       .Sum(x => (decimal?) x.TotalAmount) ?? 0)
                                   + (_context.PaymentVouchers.Where(x => x.CustomerId == c.Id)
                                       .Sum(x => (decimal?) x.Paid) ?? 0) -
                                   (_context.ReceiptVouchers.Where(x => x.CustomerId == c.Id)
                                       .Sum(x => (decimal?) x.Received) ?? 0)
                where (int) customerDebt != 0
                select new CustomersSuppliersForReportDto()
                {
                    Id = c.Id,
                    Name = c.Name,
                    PhoneNumber = c.PhoneNumber,
                    TotalDebt = customerDebt,
                    Address = c.Address,
                    Email = au.Email,
                };
            decimal totalDebt = 0;
            foreach (var c in await customers.ToListAsync())
            {
                totalDebt += c.TotalDebt;
            }
            var reportCustomer=new ReportCustomerSupplierDebtDto()
            {
                Targets = await customers.GetPagedAsync(pageIndex,pageSize),
                TotalDebt = totalDebt
            };
            return new ApiResult<ReportCustomerSupplierDebtDto>(HttpStatusCode.OK,reportCustomer);
        }

        public async Task<ApiResult<ReportCustomerSupplierDebtDto>> GetAllSupplierDebtAsync(int pageIndex,int pageSize)
        {
            var suppliers =await Task.Run(()=> (from s in _context.Suppliers
                join e in _context.Employees on s.EmployeeId equals e.Id
                let totalDebt = (_context.PurchaseOrders.Where(x => x.SupplierId == s.Id
                                                                    && x.TransactionStatusId !=
                                                                    GlobalProperties.CancelTransactionId)
                                    .Sum(x => (decimal?) x.TotalAmount) ?? 0)
                                - (_context.PaymentVouchers.Where(x => x.SupplierId == s.Id)
                                    .Sum(x => (decimal?) x.Paid) ?? 0) +
                                (_context.ReceiptVouchers.Where(x => x.SupplierId == s.Id)
                                    .Sum(x => (decimal?) x.Received) ?? 0)
                where (int) totalDebt != 0
                select new CustomersSuppliersForReportDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    PhoneNumber = s.PhoneNumber,
                    TotalDebt = totalDebt,
                    Address = s.Address,
                    Email = s.Email
                }));
            decimal totalDebt = 0;
            foreach (var s in await suppliers.ToListAsync())
            {
                totalDebt += s.TotalDebt;
            }
            var reportSupplier=new ReportCustomerSupplierDebtDto()
            {
                Targets = await suppliers.GetPagedAsync(pageIndex,pageSize),
                TotalDebt = totalDebt
            };
            return new ApiResult<ReportCustomerSupplierDebtDto>(HttpStatusCode.OK,reportSupplier);
        }

        public async Task<ApiResult<OverviewReport>> GetOverviewReport(string accountId)
        {
            var checkEmployee = await _context.Employees.Where(x => x.AppuserId.ToString() == accountId)
                .SingleOrDefaultAsync();
            if(checkEmployee==null) return new ApiResult<OverviewReport>(HttpStatusCode.NotFound,$"Lỗi tài khoản đăng nhập");
            var waitingOrder = await _context.Orders
                .Where(x => x.TransactionStatusId == GlobalProperties.WaitingTransactionId).CountAsync();
            var unfinishedOrder = await _context.Orders
                .Where(x => x.TransactionStatusId == GlobalProperties.TradingTransactionId || x.TransactionStatusId==GlobalProperties.ShippingTransactionId
                    || x.TransactionStatusId==GlobalProperties.InventoryTransactionId && x.BranchId==checkEmployee.BranchId)
                .CountAsync();
            var unfinishedPurchaseOrder = await _context.PurchaseOrders
                .Where(x => x.TransactionStatusId == GlobalProperties.TradingTransactionId || x.TransactionStatusId==GlobalProperties.InventoryTransactionId 
                            && x.BrandId==checkEmployee.BranchId).CountAsync();
            var waitingForExport =
                await _context.Orders.Where(x => x.TransactionStatusId == GlobalProperties.InventoryTransactionId && x.BranchId==checkEmployee.BranchId).CountAsync();
            var waitingForImport = await _context.PurchaseOrders
                .Where(x => x.TransactionStatusId == GlobalProperties.InventoryTransactionId && x.BrandId==checkEmployee.BranchId).CountAsync();
            var totalInventory = await (from s in _context.Stocks
                join w in _context.Warehouses on s.WarehouseId equals w.Id
                join b in _context.Branches on w.BranchId equals b.Id
                where w.Id == GlobalProperties.MainWarehouseId && b.Id == w.BranchId && s.RealQuantity > 0
                select s.RealQuantity).SumAsync();
            return new ApiResult<OverviewReport>(HttpStatusCode.OK, new OverviewReport()
            {
                TotalInventory = totalInventory,
                UnfinishedOrder = unfinishedOrder,
                WaitingOrder = waitingOrder,
                UnfinishedPurchaseOrder = unfinishedPurchaseOrder,
                WaitingForExport = waitingForExport,
                WaitingForImport = waitingForImport
            });
        }

        public async Task<ApiResult<CashBookReportDto>> GetCashBookReport(DateTime fromDate, DateTime toDate,int pageIndex, int pageSize, string accountId)
        {
            var checkEmployee = await _context.Employees.Where(x => x.AppuserId.ToString() == accountId)
                .SingleOrDefaultAsync();
            if(checkEmployee==null) return new ApiResult<CashBookReportDto>(HttpStatusCode.NotFound,"Lỗi tài khoản đăng nhập");
            var totalReceivedPrevious = await _context.ReceiptVouchers.Where(x => x.ReceivedDate < fromDate && x.BranchId==checkEmployee.BranchId).SumAsync(x=>x.Received);
            var totalPaidPrevious = await _context.PaymentVouchers.Where(x => x.PaymentDate < fromDate && x.BranchId==checkEmployee.BranchId).SumAsync(x => x.Paid);
            var totalReceived = await _context.ReceiptVouchers
                .Where(x => x.ReceivedDate >= fromDate && x.ReceivedDate <= toDate && x.BranchId==checkEmployee.BranchId).SumAsync(x => x.Received);
            var totalPaid = await _context.PaymentVouchers
                .Where(x => x.PaymentDate >= fromDate && x.PaymentDate <= toDate && x.BranchId==checkEmployee.BranchId).SumAsync(x => x.Paid);
            var results=new CashBookReportDto()
            {
                SurplusBeginningValue = totalReceivedPrevious-totalPaidPrevious,
                TotalReceivedValue = totalReceived,
                TotalPaymentValue = totalPaid,
                EndingStocksValue = totalReceivedPrevious-totalPaidPrevious+totalReceived-totalPaid,
                Rows = new PagedResult<CashBookRowReportDto>()
            };
            var stockBookRows = new List<CashBookRowReportDto>();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var resultReceiptVoucher = await connection.QueryAsync<CashBookRowReportDto>(
                    @"EXEC prGetReceiptVoucherForCashBookReport @fromDate=@FromDate,@toDate=@ToDate,@branchId=@BranchId",
                    new {FromDate = fromDate.ToString("yyyy/MM/dd"), ToDate = toDate.ToString("yyyy/MM/dd"),BranchId=checkEmployee.BranchId});
                stockBookRows.AddRange(resultReceiptVoucher);
                var resultPaymentVoucher = await connection.QueryAsync<CashBookRowReportDto>(
                    @"EXEC prGetPaymentVoucherForCashBookReport @fromDate=@FromDate,@toDate=@ToDate,@branchId=@BranchId",
                    new {FromDate = fromDate.ToString("yyyy/MM/dd"), ToDate = toDate.ToString("yyyy/MM/dd"),BranchId=checkEmployee.BranchId});
                stockBookRows.AddRange(resultPaymentVoucher);

            }
            results.Rows = stockBookRows.MapPage(pageIndex, pageSize);
            return new ApiResult<CashBookReportDto>(HttpStatusCode.OK,results);
        }

        public async Task<ApiResult<StockBookReportDto>> GetStockBookReport(DateTime dateTime, int pageIndex, int pageSize, string accountId)
        {
            var checkEmployee = await _context.Employees.Where(x => x.AppuserId.ToString() == accountId)
                .SingleOrDefaultAsync();
            if(checkEmployee==null) return new ApiResult<StockBookReportDto>(HttpStatusCode.NotFound,$"Lỗi tài khoản đăng nhập");
            var reportRows=new List<StockBookRowReportDto>();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                var rows = await connection.QueryAsync <StockBookRowReportDto>(
                    @"EXEC prGetStockBookReports @warehouseId=@WarehouseId,@dateTime=@DateTime",
                    new {WarehouseId = GlobalProperties.MainWarehouseId,DateTime=dateTime});
                reportRows.AddRange(rows?? new List<StockBookRowReportDto>());
            }
            var stockBookReport=new StockBookReportDto()
            {
                RealTotalInventory = reportRows.Select(x=>x.RealInventoryQuantity).Sum(),
                SystemTotalInventory = reportRows.Select(x=>x.SystemInventoryQuantity).Sum(),
                RealTotalInventoryValue = reportRows.Select(x=>x.TotalInventoryValue).Sum(),
                SystemTotalInventoryValue = reportRows.Select(x=>x.TotalSystemValue).Sum(),
                Rows = reportRows.MapPage(pageIndex,pageSize)
            };
            return new ApiResult<StockBookReportDto>(HttpStatusCode.OK,stockBookReport);
        }

        public async Task<ApiResult<ProfitReportDto>> GetProfitReport(DateTime fromDate, DateTime toDate, string accountId)
        {
            var checkEmployee = await _context.Employees.Where(x => x.AppuserId.ToString() == accountId)
                .SingleOrDefaultAsync();
            if(checkEmployee==null) return new ApiResult<ProfitReportDto>(HttpStatusCode.NotFound,$"Lỗi tài khoản đăng nhập");
            var salesRevenue = await (from o in _context.Orders
                join od in _context.OrderDetails on o.Id equals od.OrderId
                where o.DateCreated >= fromDate && o.DateCreated <= toDate
                select (od.UnitPrice * od.Quantity)).SumAsync();
            return new ApiResult<ProfitReportDto>(HttpStatusCode.OK,new ProfitReportDto()
            {
                SalesRevenue = salesRevenue
            });
        }
    }
}