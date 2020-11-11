using System.Threading.Tasks;
using eQACoLTD.Data.DBContext;
using System.Linq;
using System.Net;
using eQACoLTD.Application.Common;
using eQACoLTD.Application.Extensions;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Customer.Queries;
using eQACoLTD.ViewModel.Product.Supplier.Queries;
using eQACoLTD.ViewModel.Report.Queries;
using Microsoft.EntityFrameworkCore;
using NotImplementedException = System.NotImplementedException;

namespace eQACoLTD.Application.Report
{
    public class ReportService:IReportService
    {
        private readonly AppIdentityDbContext _context;

        public ReportService(AppIdentityDbContext context)
        {
            _context = context;
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
        
        
    }
}