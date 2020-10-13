using eQACoLTD.Data.DBContext;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Product.Supplier.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eQACoLTD.Application.Configurations;
using eQACoLTD.Application.Extensions;
using eQACoLTD.Data.Entities;
using eQACoLTD.Utilities.Extensions;
using eQACoLTD.ViewModel.Product.Supplier.Handlers;
using Microsoft.EntityFrameworkCore;

namespace eQACoLTD.Application.Product.Supplier
{
    public class SupplierService : ISupplierService
    {
        private readonly AppIdentityDbContext _context;
        private readonly ILoggerManager _logger;
        public SupplierService(AppIdentityDbContext context,ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<ApiResult<PagedResult<SuppliersDto>>> GetSuppliersPagingAsync(int pageIndex, int pageSize)
        {
            var suppliers = await (from s in _context.Suppliers
                join employee in _context.Employees on s.EmployeeId equals employee.Id
                into EmployeeGroup
                from e in EmployeeGroup.DefaultIfEmpty()
                where s.IsDelete==false
                select new SuppliersDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    EmployeeName = e.Name,
                    PhoneNumber = s.PhoneNumber
                }).GetPagedAsync(pageIndex,pageSize);
            if(suppliers==null) return new ApiResult<PagedResult<SuppliersDto>>(HttpStatusCode.NoContent);
            return new ApiResult<PagedResult<SuppliersDto>>(HttpStatusCode.OK,suppliers);
        }

        public async Task<ApiResult<SupplierDto>> GetSupplierAsync(string supplierId)
        {
            var checkSupplier = await _context.Suppliers.FindAsync(supplierId);
            if(checkSupplier==null) return new ApiResult<SupplierDto>(HttpStatusCode.NotFound,$"Không tìm thấy nhà cung cấp có mã: {supplierId}");
            var totalPurchaseOrder = await (from po in _context.PurchaseOrders
                                            where po.SupplierId == supplierId
                                            select po.TotalAmount).SumAsync();
            var totalReceiptVoucher = await (from rv in _context.ReceiptVouchers
                                             where rv.SupplierId == supplierId
                                             select rv.Received).SumAsync();
            var totalPaymentVoucher = await (from pv in _context.PaymentVouchers
                                             where pv.SupplierId == supplierId
                                             select pv.Paid).SumAsync();
            var supplier=await (from s in _context.Suppliers
                join employee in _context.Employees on s.EmployeeId equals employee.Id
                into EmployeesGroup
                from e in EmployeesGroup.DefaultIfEmpty()
                where s.Id==supplierId && s.IsDelete==false
                let totalDebt=(from po in _context.PurchaseOrders
                        join pod in _context.PurchaseOrderDetails on po.Id equals pod.PurchaseOrderId
                        where po.SupplierId==s.Id select pod.UnitPrice*pod.Quantity).Sum()-
                              (from po in _context.PurchaseOrders join pv in _context.PaymentVouchers
                                      on po.Id equals pv.PurchaseOrderId
                                      where po.SupplierId==s.Id select pv.Paid).Sum()+
                              (from rv in _context.ReceiptVouchers where rv.SupplierId==supplierId
                                      select rv.Received).Sum()
                select new SupplierDto()
                {
                    Id=s.Id,
                    Address = s.Address,
                    Description = s.Description,
                    Email = s.Email,
                    Fax = s.Fax,
                    Name = s.Name,
                    Website = s.Website,
                    EmployeeName = e.Name,
                    PhoneNumber = s.PhoneNumber,
                    TotalDebt = totalPurchaseOrder+totalReceiptVoucher-totalPaymentVoucher
                }).SingleOrDefaultAsync();
            return new ApiResult<SupplierDto>(HttpStatusCode.OK,supplier);

        }

        public async Task<ApiResult<string>> CreateSupplierAsync(SupplierForCreationDto creationDto)
        {
            try
            {
                var sequenceNumber = await _context.Suppliers.CountAsync();
                var generateId = IdentifyGenerator.GenerateSupplierId(sequenceNumber + 1);
                var supplier = ObjectMapper.Mapper.Map<SupplierForCreationDto, Data.Entities.Supplier>(creationDto);
                supplier.Id = generateId;
                await _context.Suppliers.AddAsync(supplier);
                await _context.SaveChangesAsync();
                return new ApiResult<string>(HttpStatusCode.OK){ResultObj = generateId};
            }
            catch (Exception e)
            {
                _logger.LogInfo("Có lỗi khi thêm nhà cung cấp");
                return new ApiResult<string>(HttpStatusCode.InternalServerError,$"Có lỗi khi thêm nhà cung cấp");
            }
        }

        public async Task<ApiResult<string>> DeleteSupplierAsync(string supplierId)
        {
            var checkSupplier = await _context.Suppliers.FindAsync(supplierId);
            if(checkSupplier==null) return new ApiResult<string>(HttpStatusCode.NotFound,$"Không tìm thấy nhà cung cấp có mã: {supplierId}");
            checkSupplier.IsDelete = true;
            await _context.SaveChangesAsync();
            return new ApiResult<string>(HttpStatusCode.OK){ResultObj = supplierId};
        }

        public async Task<ApiResult<PagedResult<SupplierImportHistoriesDto>>> GetSupplierImportHistoriesPagingAsync(string supplierId,int pageIndex,int pageSize)
        {
            var checkSupplier = await _context.Suppliers.FindAsync(supplierId);
            if(checkSupplier==null) 
                return new ApiResult<PagedResult<SupplierImportHistoriesDto>>(HttpStatusCode.NotFound,$"Không tìm thấy nhà cung cấp có mã: {supplierId}");
            var supplierHistories = await (from po in _context.PurchaseOrders
                join paymentvoucher in _context.PaymentVouchers on po.Id equals paymentvoucher.PurchaseOrderId
                into PaymentVoucherGroup
                from pv in PaymentVoucherGroup.DefaultIfEmpty()
                join ps in _context.PaymentStatuses on po.PaymentStatusId equals ps.Id
                join ts in _context.TransactionStatuses on po.TransactionStatusId equals ts.Id
                join b in _context.Branches on po.BrandId equals b.Id
                orderby po.Id descending
                    let totalPo = _context.PurchaseOrderDetails.Where(x => x.PurchaseOrderId == po.Id)
                        .Sum(x => x.Quantity * x.UnitPrice)
                where po.SupplierId==supplierId
                select new SupplierImportHistoriesDto()
                {
                    DateCreated = po.DateCreated,
                    PaymentStatus = ps.Name,
                    TotalAmount = totalPo,
                    TransactionStatus = ts.Name,
                    PurchaseOrderId = po.Id,
                    BranchName = b.Name
                }
                ).GetPagedAsync(pageIndex,pageSize);
            return new ApiResult<PagedResult<SupplierImportHistoriesDto>>(HttpStatusCode.OK,supplierHistories);
        }
    }
}
