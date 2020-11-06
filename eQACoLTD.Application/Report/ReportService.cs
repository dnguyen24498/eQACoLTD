using System.Threading.Tasks;
using eQACoLTD.Data.DBContext;
using System.Linq;
using System.Net;
using eQACoLTD.Application.Common;
using eQACoLTD.Application.Extensions;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Customer.Queries;
using eQACoLTD.ViewModel.Product.Supplier.Queries;
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
        public async Task<ApiResult<PagedResult<CustomerDto>>> GetAllCustomerDebtAsync(int pageIndex,int pageSize)
        {
            var customers = await (from c in _context.Customers
                join employee in _context.Employees on c.EmployeeId equals employee.Id
                into EmployeeGroup
                from e in EmployeeGroup.DefaultIfEmpty()
                join appuser in _context.AppUsers on c.AppUserId equals appuser.Id
                into AppUserGroup
                from au in AppUserGroup.DefaultIfEmpty()
                join ct in _context.CustomerTypes on c.CustomerTypeId equals ct.Id
                let customerDebt = (_context.Orders.Where(x => x.CustomerId == c.Id && x.TransactionStatusId!=GlobalProperties.CancelTransactionId
                                   && x.TransactionStatusId!=GlobalProperties.WaitingTransactionId).Sum(x => (decimal?)x.TotalAmount)??0)
                                   + (_context.PaymentVouchers.Where(x => x.CustomerId == c.Id).Sum(x => (decimal?)x.Paid)??0) -
                                   (_context.ReceiptVouchers.Where(x => x.CustomerId == c.Id).Sum(x => (decimal?)x.Received)??0)
                where (int)customerDebt!=0
                select new CustomerDto()
                {
                    Id = c.Id,
                    Name = c.Name,
                    PhoneNumber = c.PhoneNumber,
                    TotalDebt = customerDebt,
                    Address = c.Address,
                    Description = c.Description,
                    Dob = c.Dob,
                    Fax = c.Fax,
                    Gender = c.Gender,
                    Website = c.Website,
                    EmployeeName = e.Name,
                    IsDelete = c.IsDelete,
                    UserName = au.UserName,
                    CustomerTypeName = ct.Name
                }).GetPagedAsync(pageIndex, pageSize);
            return new ApiResult<PagedResult<CustomerDto>>(HttpStatusCode.OK,customers);
        }

        public async Task<ApiResult<PagedResult<SupplierDto>>> GetAllSupplierDebtAsync(int pageIndex,int pageSize)
        {
            var suppliers = await (from s in _context.Suppliers
                join e in _context.Employees on s.EmployeeId equals e.Id
                let totalDebt = (_context.PurchaseOrders.Where(x => x.SupplierId == s.Id 
                                    && x.TransactionStatusId!=GlobalProperties.CancelTransactionId)
                                    .Sum(x => (decimal?) x.TotalAmount) ?? 0)
                                - (_context.PaymentVouchers.Where(x => x.SupplierId == s.Id)
                                    .Sum(x => (decimal?) x.Paid) ?? 0) +
                                (_context.ReceiptVouchers.Where(x => x.SupplierId == s.Id)
                                    .Sum(x => (decimal?) x.Received) ?? 0)
                                where (int)totalDebt!=0
                select new SupplierDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    PhoneNumber = s.PhoneNumber,
                    TotalDebt = totalDebt,
                    Address = s.Address,
                    Description = s.Description,
                    Email = s.Email,
                    Fax = s.Fax,
                    Website = s.Website,
                    EmployeeName = e.Name
                }).GetPagedAsync(pageIndex, pageSize);
            return new ApiResult<PagedResult<SupplierDto>>(HttpStatusCode.OK,suppliers);
        }
    }
}