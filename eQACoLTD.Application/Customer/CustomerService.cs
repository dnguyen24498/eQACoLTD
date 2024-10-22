﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eQACoLTD.Application.Common;
using eQACoLTD.Application.Configurations;
using eQACoLTD.Application.Extensions;
using eQACoLTD.Data.DBContext;
using eQACoLTD.Data.Entities;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.Customer.Handlers;
using eQACoLTD.ViewModel.Customer.Queries;
using Microsoft.EntityFrameworkCore;
using NotImplementedException = System.NotImplementedException;

namespace eQACoLTD.Application.Customer
{
    public class CustomerService:ICustomerService
    {
        private readonly AppIdentityDbContext _context;

        public CustomerService(AppIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<PagedResult<CustomersDto>>> GetCustomersPagingAsync(int pageIndex, int pageSize)
        {
            var customers = await (from c in _context.Customers
                join employee in _context.Employees on c.EmployeeId equals employee.Id
                    into EmployeeGroup
                from e in EmployeeGroup.DefaultIfEmpty()
                join appuser in _context.AppUsers on c.AppUserId equals appuser.Id
                    into AppUserGroup
                from au in AppUserGroup.DefaultIfEmpty()
                where c.IsDelete==false
                select new CustomersDto()
                {
                    Id = c.Id,
                    Name = c.Name,
                    EmployeeName = e.Name,
                    PhoneNumber = c.PhoneNumber,
                    UserName = au.UserName
                }).GetPagedAsync(pageIndex,pageSize);
            if(customers==null) return new ApiResult<PagedResult<CustomersDto>>(HttpStatusCode.NoContent);
            return new ApiResult<PagedResult<CustomersDto>>(HttpStatusCode.OK,customers);
        }

        public async Task<ApiResult<CustomerDto>> GetCustomerAsync(string customerId)
        {
            var totalAmountOrders = await _context.Orders.Where(x => x.CustomerId == customerId && x.TransactionStatusId!=GlobalProperties.CancelTransactionId
                && x.TransactionStatusId!=GlobalProperties.WaitingTransactionId).SumAsync(x=>x.TotalAmount);
            var totalPaymentVouchers = await _context.PaymentVouchers.Where(x => x.CustomerId == customerId).SumAsync(x => x.Paid);
            var totalReceiptVouchers = await _context.ReceiptVouchers.Where(x => x.CustomerId == customerId).SumAsync(x => x.Received);
            var customer = await (from c in _context.Customers
                                  join employee in _context.Employees on c.EmployeeId equals employee.Id
                                      into EmployeeGroup
                                  from e in EmployeeGroup.DefaultIfEmpty()
                                  join appuser in _context.AppUsers on c.AppUserId equals appuser.Id
                                      into AppUserGroup
                                  from au in AppUserGroup.DefaultIfEmpty()
                                  join customertype in _context.CustomerTypes on c.CustomerTypeId equals customertype.Id
                                      into CustomerTypeGroup
                                  from ct in CustomerTypeGroup.DefaultIfEmpty()
                                  where c.Id == customerId && c.IsDelete == false
                                  select new CustomerDto()
                                  {
                                      Address = c.Address,
                                      Description = c.Description,
                                      Dob = c.Dob,
                                      Fax = c.Fax,
                                      Gender = c.Gender,
                                      Id = c.Id,
                                      Name = c.Name,
                                      Website = c.Website,
                                      EmployeeName = e.Name,
                                      PhoneNumber = c.PhoneNumber,
                                      IsDelete = c.IsDelete,
                                      UserName = au.UserName,
                                      CustomerTypeName = ct.Name,
                                      TotalDebt = totalAmountOrders + totalPaymentVouchers - totalReceiptVouchers
                                  }).SingleOrDefaultAsync();
            if(customer==null) return new ApiResult<CustomerDto>(HttpStatusCode.NotFound,$"Không tìm thấy khách hàng có mã: {customerId}");
            return new ApiResult<CustomerDto>(HttpStatusCode.OK,customer);
        }

        public async Task<ApiResult<PagedResult<CustomerHistoriesDto>>> GetCustomerHistoriesAsync(string customerId, int pageIndex, int pageSize)
        {
            var checkCustomer = await _context.Customers.FindAsync(customerId);
            if (checkCustomer == null||checkCustomer.IsDelete) 
                return new ApiResult<PagedResult<CustomerHistoriesDto>>(HttpStatusCode.NotFound,$"Không tìm thấy khách hàng có mã: {customerId}");
            var customerHistories = await (from c in _context.Customers
                join sr in _context.Orders on c.Id equals sr.CustomerId
                join pm in _context.PaymentStatuses on sr.PaymentStatusId equals pm.Id
                join ts in _context.TransactionStatuses on sr.TransactionStatusId equals ts.Id
                orderby sr.DateCreated descending 
                let totalAmount = (from srd in _context.OrderDetails
                    where srd.OrderId == sr.Id
                    select srd.Quantity * srd.UnitPrice).Sum()
                where sr.CustomerId==customerId
                select new CustomerHistoriesDto()
                {
                    OrderDate = sr.DateCreated,
                    PaymentStatus = pm.Name,
                    TotalAmount = totalAmount,
                    TransactionStatus = ts.Name,
                    SaleReceiptId = sr.Id
                }).GetPagedAsync(pageIndex, pageSize);
            if(customerHistories==null) return new ApiResult<PagedResult<CustomerHistoriesDto>>(HttpStatusCode.NoContent);
            return new ApiResult<PagedResult<CustomerHistoriesDto>>(HttpStatusCode.OK,customerHistories);
        }

        public async Task<ApiResult<string>> CreateCustomerAsync(CustomerForCreationDto creationDto,string accountId)
        {
            try
            {
                var checkEmployee = await _context.Employees.Where(x => x.AppuserId.ToString() == accountId)
                    .SingleOrDefaultAsync();
                if(checkEmployee==null) return new ApiResult<string>(HttpStatusCode.NotFound,"Lỗi tài khoản đăng nhập");
                var sequenceNumber = await _context.Customers.CountAsync();
                var customerId = IdentifyGenerator.GenerateCustomerId(sequenceNumber + 1);
                var customer = ObjectMapper.Mapper.Map<Data.Entities.Customer>(creationDto);
                customer.Id = customerId;
                customer.EmployeeId = checkEmployee.Id;
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
                return new ApiResult<string>(HttpStatusCode.OK){ResultObj = customerId,Message = "Tạo mới khách hàng thành công"};
            }
            catch
            {
                return new ApiResult<string>(HttpStatusCode.InternalServerError,"Có lỗi khi tạo khách hàng");
            }
        }

        public async Task<ApiResult<string>> DeleteCustomerAsync(string customerId)
        {
            var checkCustomer = await _context.Customers.FindAsync(customerId);
            if (checkCustomer == null) return new ApiResult<string>(HttpStatusCode.NotFound) { ResultObj = customerId,Message = $"Không tìm thấy khách hàng có mã: {customerId}"};
            checkCustomer.IsDelete = true;
            await _context.SaveChangesAsync();
            return new ApiResult<string>(HttpStatusCode.OK) { ResultObj = customerId,Message = "Xóa khách hàng thành công"};
        }

        public async Task<ApiResult<IEnumerable<CustomerDto>>> SearchCustomerAsync(string customerName)
        {
            var customers = await (from c in _context.Customers
                join employee in _context.Employees on c.EmployeeId equals employee.Id
                    into EmployeeGroup
                from e in EmployeeGroup.DefaultIfEmpty()
                join appuser in _context.AppUsers on c.AppUserId equals appuser.Id
                    into AppUserGroup
                from au in AppUserGroup.DefaultIfEmpty()
                join customertype in _context.CustomerTypes on c.CustomerTypeId equals customertype.Id
                    into CustomerTypeGroup
                from ct in CustomerTypeGroup.DefaultIfEmpty()
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
                where c.Name.ToLower().Contains(customerName.ToLower()) && c.IsDelete == false
                select new CustomerDto()
                {
                    Address = c.Address,
                    Description = c.Description,
                    Dob = c.Dob,
                    Fax = c.Fax,
                    Gender = c.Gender,
                    Id = c.Id,
                    Name = c.Name,
                    Website = c.Website,
                    EmployeeName = e.Name,
                    IsDelete = c.IsDelete,
                    UserName = au.UserName,
                    PhoneNumber = c.PhoneNumber,
                    CustomerTypeName = ct.Name,
                    TotalDebt = customerDebt,
                    Email = string.IsNullOrEmpty(c.Email)?au.Email:c.Email
                }).ToListAsync();
            return new ApiResult<IEnumerable<CustomerDto>>()
            {
                Code = HttpStatusCode.OK,
                ResultObj = customers
            };
        }

    }
}