using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eQACoLTD.Data.DBContext;
using System.Linq;
using System.Net;
using eQACoLTD.Application.Common;
using eQACoLTD.Application.Configurations;
using eQACoLTD.Application.Extensions;
using eQACoLTD.Data.Entities;
using eQACoLTD.Utilities.Extensions;
using eQACoLTD.ViewModel.Common;
using eQACoLTD.ViewModel.System.Account.Handlers;
using eQACoLTD.ViewModel.System.Account.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eQACoLTD.Application.System.Account
{
    public class AccountService:IAccountService
    {
        private readonly AppIdentityDbContext _context;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountService(AppIdentityDbContext context,
            RoleManager<AppRole> roleManager,UserManager<AppUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<ApiResult<PagedResult<AccountsDto>>> GetAccountsPagingAsync(int pageIndex,int pageSize)
        {
            var accounts = await (from au in _context.AppUsers
                join emp in _context.Employees on au.Id equals emp.AppuserId
                into EmployeeGroup
                from e in EmployeeGroup.DefaultIfEmpty()
                join cus in _context.Customers on au.Id equals cus.AppUserId
                into CustomerGroup
                from c in CustomerGroup.DefaultIfEmpty()
                join sup in _context.Suppliers on au.Id equals sup.AppUserId
                into SupplierGroup
                from s in SupplierGroup.DefaultIfEmpty()
                select new AccountsDto()
                {
                    Id = au.Id,
                    UserName = au.UserName,
                    Email = au.Email,
                    EmployeeName = e.Name,
                    CustomerName = c.Name,
                    SupplierName = s.Name,
                    PhoneNumber = au.PhoneNumber
                }).GetPagedAsync(pageIndex,pageSize);
            return new ApiResult<PagedResult<AccountsDto>>(HttpStatusCode.OK,accounts);
        }

        public async Task<ApiResult<AccountDto>> GetAccountAsync(Guid userId)
        {
            var checkAccount = await _userManager.FindByIdAsync(userId.ToString(("D")));
            if (checkAccount == null) 
                return new ApiResult<AccountDto>(HttpStatusCode.NotFound,$"Không tìm thấy tài khoản có mã: {userId}");
            var roles = await _userManager.GetRolesAsync(checkAccount);
            var accountRoles = new List<AccountRolesDto>();
            foreach (var r in roles)
            {
                var role = await _roleManager.FindByNameAsync(r);
                if(role!=null) accountRoles.Add(ObjectMapper.Mapper.Map<AppRole,AccountRolesDto>(role));
            }
            var accountDetail = await (from au in _context.AppUsers
                join emp in _context.Employees on au.Id equals emp.AppuserId
                    into EmployeeGroup
                from e in EmployeeGroup.DefaultIfEmpty()
                join cus in _context.Customers on au.Id equals cus.AppUserId
                    into CustomerGroup
                from c in CustomerGroup.DefaultIfEmpty()
                join sup in _context.Suppliers on au.Id equals sup.AppUserId
                    into SupplierGroup
                from s in SupplierGroup.DefaultIfEmpty()
                where au.Id==userId
                select new AccountDto()
                {
                    Id = au.Id,
                    Address = e.Address != null
                        ? e.Address
                        : (c.Address != null ? c.Address : (s.Address != null ? s.Address : "")),
                    Email = au.Email,
                    Name = e.Name != null ? e.Name : (c.Name != null ? c.Name : (s.Name != null ? s.Name : "")),
                    PhoneNumber = au.PhoneNumber != null
                        ? au.PhoneNumber
                        : (e.PhoneNumber != null
                            ? e.PhoneNumber
                            : (c.PhoneNumber != null
                                ? c.PhoneNumber
                                : (
                                    s.PhoneNumber != null ? s.PhoneNumber : ""))),
                    UserName = au.UserName,
                    InRoles = accountRoles
                }).SingleOrDefaultAsync();
            return new ApiResult<AccountDto>(HttpStatusCode.OK,accountDetail);
        }

        public async Task<ApiResult<Guid>> AddRoleAsync(Guid userId, Guid roleId,string accountId)
        {
            var checkAccount = await _userManager.FindByIdAsync(accountId);
            var user = await _userManager.FindByIdAsync(userId.ToString("D"));
            if (user == null) return new ApiResult<Guid>(HttpStatusCode.NotFound,$"Không tìm thấy tài khoản có mã: {userId}");
            if(checkAccount.Id==user.Id) return new ApiResult<Guid>(HttpStatusCode.BadRequest,$"Tài khoản đã ở quyền cao nhất của hệ thống");
            if(user.UserName.ToLower()=="admin") return new ApiResult<Guid>(HttpStatusCode.BadRequest,$"Không được chỉnh sửa quyền cho tài khoản có mức độ phân quyền cao nhất");
            var role = await _roleManager.FindByIdAsync(roleId.ToString("D"));
            if(role==null) return new ApiResult<Guid>(HttpStatusCode.NotFound,$"Không tìm thấy quyền có mã: {roleId}");
            var isInRole = await _userManager.IsInRoleAsync(user, role.Name);
            if(isInRole) return new ApiResult<Guid>(HttpStatusCode.BadRequest,"Tài khoản đã có quyền này");
            var result=await _userManager.AddToRoleAsync(user, role.Name);
            if(result.Succeeded)
                return new ApiResult<Guid>(HttpStatusCode.OK,roleId)
                {
                    Message = "Đã thêm quyền"
                };
            return new ApiResult<Guid>(HttpStatusCode.NotModified,$"Có lỗi khi thêm quyền");
        }

        public async Task<ApiResult<Guid>> RemoveRoleAsync(Guid userId, Guid roleId,string accountId)
        {
            var checkAccount = await _userManager.FindByIdAsync(accountId);
            var user = await _userManager.FindByIdAsync(userId.ToString("D"));
            if (user == null) return new ApiResult<Guid>(HttpStatusCode.NotFound,$"Không tìm thấy tài khoản có mã: {userId}");
            if(checkAccount.Id==user.Id) return new ApiResult<Guid>(HttpStatusCode.BadRequest,$"Tài khoản đã ở quyền cao nhất của hệ thống");
            if(user.UserName.ToLower()=="admin") return new ApiResult<Guid>(HttpStatusCode.BadRequest,$"Không được chỉnh sửa quyền cho tài khoản có mức độ phân quyền cao nhất");
            var role = await _roleManager.FindByIdAsync(roleId.ToString("D"));
            if(role==null) return new ApiResult<Guid>(HttpStatusCode.NotFound,$"Không tìm thấy quyền có mã: {roleId}");
            var isInRole = await _userManager.IsInRoleAsync(user, role.Name);
            if(!isInRole)  return new ApiResult<Guid>(HttpStatusCode.NotFound,$"Tài khoản không có quyền này");
            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if(result.Succeeded)
                return new ApiResult<Guid>(HttpStatusCode.OK,roleId)
                {
                    Message = "Đã xóa quyền"
                };
            return new ApiResult<Guid>(HttpStatusCode.NotModified,$"Có lỗi khi xóa quyền");
        }

        public async Task<ApiResult<IEnumerable<AccountRolesDto>>> NotInRolesAsync(Guid userId)
        {
            var checkUser = await _userManager.FindByIdAsync(userId.ToString("D"));
            if(checkUser==null) 
                return new ApiResult<IEnumerable<AccountRolesDto>>(HttpStatusCode.NotFound,$"Không tìm thấy tài khoản có mã: {userId}");
            var roles = await _userManager.GetRolesAsync(checkUser);
            var rolesConvert = new List<AppRole>();
            foreach (var r in roles)
            {
                var findRole = await _roleManager.FindByNameAsync(r);
                if(findRole!=null) rolesConvert.Add(findRole);
            }

            var notInRoles = (await _roleManager.Roles.ToListAsync()).Except(rolesConvert).ToList();
            return new ApiResult<IEnumerable<AccountRolesDto>>(HttpStatusCode.OK,
                ObjectMapper.Mapper.Map<List<AppRole>,List<AccountRolesDto>>(notInRoles));
        }

        public async Task<ApiResult<int>> AddProductToCart(string customerId, string productId)
        {
            var checkCustomerAccount = await _context.AppUsers.Where(x => x.Id.ToString() == customerId).SingleOrDefaultAsync();
            if (checkCustomerAccount == null) return new ApiResult<int>(HttpStatusCode.NotFound, $"Không tìm thấy tài khoản");
            var checkCustomer = await _context.Customers.Where(x => x.AppUserId == checkCustomerAccount.Id).SingleOrDefaultAsync();
            if(checkCustomer==null) return new ApiResult<int>(HttpStatusCode.NotFound,$"Không tìm thấy khách hàng");
            var checkProduct = await _context.Products.FindAsync(productId);
            if(checkProduct==null) return new ApiResult<int>(HttpStatusCode.NotFound,$"Không tìm thấy sản phẩm");
            var checkCart = await _context.Carts
                .Where(x => x.AppUserId == checkCustomerAccount.Id && x.ProductId == productId).SingleOrDefaultAsync();
            if (checkCart != null)
                checkCart.Quantity++;
            else
            {
                var cart=new Cart()
                {
                    AppUserId = checkCustomerAccount.Id,
                    ProductId = checkProduct.Id,
                    Quantity = 1
                };
                await _context.Carts.AddAsync(cart);
            }

            await _context.SaveChangesAsync();
            return new ApiResult<int>(HttpStatusCode.OK,await _context.Carts.Where(x=>x.AppUserId==checkCustomerAccount.Id).CountAsync())
            {
                Message = "Đã thêm sản phẩm vào giỏ hàng"
            };
        }

        public async Task<ApiResult<CartDto>> GetCart(string customerId)
        {
            var checkCustomerAccount = await _context.AppUsers.Where(x => x.Id.ToString() == customerId).SingleOrDefaultAsync();
            if (checkCustomerAccount == null) return new ApiResult<CartDto>(HttpStatusCode.NotFound, $"Không tìm thấy tài khoản");
            var checkCustomer = await _context.Customers.Where(x => x.AppUserId == checkCustomerAccount.Id).SingleOrDefaultAsync();
            if (checkCustomer==null) return new ApiResult<CartDto>(HttpStatusCode.NotFound,$"Không tìm thấy khách hàng");
            var cartDetails = await (from c in _context.Carts
                join p in _context.Products on c.ProductId equals p.Id
                where c.AppUserId == checkCustomerAccount.Id
                select new CartDetailDto()
                {
                    Quantity = c.Quantity,
                    ProductId = c.ProductId,
                    ProductName = p.Name,
                    ImagePath = p.ProductImages.Where(x=>x.IsThumbnail==true).SingleOrDefault().Path
                }).ToListAsync();
            foreach (var item in cartDetails)
            {
                item.UnitPrice = await GetProductPrice(item.ProductId);
            }
            var cart=new CartDto()
            {
                CustomerId = checkCustomer.Id,
                ListProduct = cartDetails,
                TotalAmount = cartDetails==null||cartDetails.Count==0?0:cartDetails.Sum(x => x.Quantity * x.UnitPrice)
            };
            return new ApiResult<CartDto>(HttpStatusCode.OK,cart);
        }

        private async Task<decimal> GetProductPrice(string productId)
        {
            var product = await _context.Products.FindAsync(productId);
            var newPriceIfExists = await (from p in _context.Promotions
                join pd in _context.PromotionDetails on p.Id equals pd.PromotionId
                where p.FromDate <= DateTime.Now && DateTime.Now<=p.ToDate && pd.ProductId==productId
                select pd).SingleOrDefaultAsync();
            if (newPriceIfExists != null)
                return newPriceIfExists.DiscountType == "%"
                    ? product.RetailPrice - (product.RetailPrice * newPriceIfExists.DiscountValue / 100)
                    : product.RetailPrice - newPriceIfExists.DiscountValue;
            return product.RetailPrice;
        }

        public async Task<ApiResult<string>> DeleteProductFromCart(string customerId, string productId)
        {
            var checkCustomerAccount = await _context.AppUsers.Where(x => x.Id.ToString() == customerId).SingleOrDefaultAsync();
            if (checkCustomerAccount == null) return new ApiResult<string>(HttpStatusCode.NotFound, $"Không tìm thấy tài khoản");
            var checkCustomer = await _context.Customers.Where(x => x.AppUserId == checkCustomerAccount.Id).SingleOrDefaultAsync();
            if (checkCustomer == null) return new ApiResult<string>(HttpStatusCode.NotFound, $"Không tìm thấy khách hàng");
            var checkProduct = await _context.Products.FindAsync(productId);
            if(checkProduct==null) return new ApiResult<string>(HttpStatusCode.NotFound,$"Không tìm thấy sản phẩm có mã: {productId}");
            var productInCart = await (from c in _context.Carts
                where c.AppUserId == checkCustomerAccount.Id && c.ProductId == checkProduct.Id
                select c).SingleOrDefaultAsync();
            if(productInCart==null) return new ApiResult<string>(HttpStatusCode.NotFound,$"Trong giỏ hàng không có sản phẩm này");
            if (productInCart.Quantity == 1) _context.Carts.Remove(productInCart);
            else productInCart.Quantity -= 1;
            await _context.SaveChangesAsync();
            return new ApiResult<string>(HttpStatusCode.OK)
            {
                ResultObj = checkProduct.Id,
                Message = "Đã xóa sản phẩm khỏi giỏ hàng"
            };
        }

        public async Task<ApiResult<AccountInfo>> GetCurrentAccountInfo(string accountId)
        {
            var checkAccount = await _context.AppUsers.Where(x => x.Id.ToString() == accountId).SingleOrDefaultAsync();
            if (checkAccount == null) return new ApiResult<AccountInfo>(HttpStatusCode.NotFound, $"Không tìm thấy tài khoản");
            var checkCustomer = await _context.Customers.Where(x => x.AppUserId == checkAccount.Id).SingleOrDefaultAsync();
            if (checkCustomer != null)
            {
                var customer = await (from c in _context.Customers
                    join au in _context.AppUsers on c.AppUserId equals au.Id
                    where c.Id==checkCustomer.Id && c.IsDelete==false    
                    select new CustomerInfo()
                    {
                        Address = c.Address,
                        Name = c.Name,
                        PhoneNumber = c.PhoneNumber ?? au.PhoneNumber,
                        Dob = c.Dob,
                        Email = string.IsNullOrEmpty(c.Email)? checkAccount.Email:c.Email,
                        Fax = c.Fax,
                        Gender = c.Gender,
                        Id = checkAccount.Id.ToString("D"),
                        Website = c.Website
                    }).SingleOrDefaultAsync();
                return new ApiResult<AccountInfo>(HttpStatusCode.OK,customer);   
            }

            var checkEmployee =
                await _context.Employees.Where(x => x.AppuserId == checkAccount.Id).SingleOrDefaultAsync();
            if (checkEmployee != null)
            {
                var employee = await (from e in _context.Employees
                    join au in _context.AppUsers on e.AppuserId equals au.Id
                    join b in _context.Branches on e.BranchId equals b.Id
                    join d in _context.Departments on e.DepartmentId equals d.Id
                    where e.Id == checkEmployee.Id && e.IsDelete == false
                    select new EmployeeInfo()
                    {
                        Address = e.Address,
                        Dob = e.Dob,
                        Email = checkAccount.Email,
                        Gender = e.Gender,
                        Id = checkAccount.Id.ToString("D"),
                        Name = e.Name,
                        BranchName = b.Name,
                        DepartmentName = d.Name,
                        PhoneNumber = string.IsNullOrEmpty(e.PhoneNumber) ? checkAccount.PhoneNumber : e.PhoneNumber
                    }).SingleOrDefaultAsync();
                return new ApiResult<AccountInfo>(HttpStatusCode.OK,employee);
            }

            var checkSupplier =
                await _context.Suppliers.Where(x => x.AppUserId == checkAccount.Id).SingleOrDefaultAsync();
            if (checkSupplier != null)
            {
                var supplier = await (from s in _context.Suppliers
                    join au in _context.AppUsers on s.AppUserId equals au.Id
                    where s.Id == checkSupplier.Id && s.IsDelete == false
                    select new SupplierInfo()
                    {
                        Address = s.Address,
                        Email = string.IsNullOrEmpty(s.Email) ? checkAccount.Email : s.Email,
                        Fax = s.Fax,
                        Id = checkAccount.Id.ToString("D"),
                        Name = s.Name,
                        Website = s.Website,
                        PhoneNumber = string.IsNullOrEmpty(s.PhoneNumber) ? checkAccount.PhoneNumber : s.PhoneNumber
                    }).SingleOrDefaultAsync();
                return new ApiResult<AccountInfo>(HttpStatusCode.OK,supplier);
            }
            return new ApiResult<AccountInfo>(HttpStatusCode.NotFound,$"Không tìm thấy người dùng liên kết với tài khoản");
        }
        public async Task<ApiResult<string>> CreateOrderFromCartAsync(string customerId)
        {
            var checkCustomerAccount = await _context.AppUsers.Where(x => x.Id.ToString() == customerId).SingleOrDefaultAsync();
            if (checkCustomerAccount == null) return new ApiResult<string>(HttpStatusCode.NotFound, $"Không tìm thấy tài khoản");
            var checkCustomer = await _context.Customers.Where(x => x.AppUserId == checkCustomerAccount.Id).SingleOrDefaultAsync();
            if (checkCustomer == null) return new ApiResult<string>(HttpStatusCode.NotFound, $"Không tìm thấy khách hàng");
            var carts = await _context.Carts.Where(x => x.AppUserId == checkCustomerAccount.Id).ToListAsync();
            if(carts==null||carts.Count==0) return new ApiResult<string>(HttpStatusCode.NotFound,$"Giỏ hàng của khách hàng đang trống");
            if(string.IsNullOrEmpty(checkCustomer.Name)||string.IsNullOrEmpty(checkCustomer.PhoneNumber)||
               string.IsNullOrEmpty(checkCustomer.Address)) 
                return new ApiResult<string>(HttpStatusCode.BadRequest,$"Hãy cập nhập thông tin cá nhân trước khi đặt hàng");
            var sequenceNumber = await _context.Orders.CountAsync();
            var orderId = IdentifyGenerator.GenerateOrderId(sequenceNumber + 1);
            var orderDetails=new List<OrderDetail>();
            foreach (var cart in carts)
            {
                orderDetails.Add(new OrderDetail()
                {
                    Id = Guid.NewGuid().ToString("D"),
                    OrderId = orderId,
                    ProductId = cart.ProductId,
                    Quantity = cart.Quantity,
                    UnitPrice = await GetProductPrice(cart.ProductId)
                });
            }
            var order=new Data.Entities.Order()
            {
                Id = orderId,
                CustomerId = checkCustomer.Id,
                TransactionStatusId = GlobalProperties.WaitingTransactionId,
                PaymentStatusId = GlobalProperties.UnpaidPaymentId,
                TotalAmount = orderDetails.Sum(x=>x.Quantity*x.UnitPrice),
                DateCreated = DateTime.Now,
                OrderDetails = orderDetails
            };
            await _context.Orders.AddAsync(order);
            _context.RemoveRange(carts);
            await _context.SaveChangesAsync();
            return new ApiResult<string>(HttpStatusCode.OK)
            {
                ResultObj = orderId,
                Message = "Đã tạo đơn hàng"
            };
        }
        

        public async Task<ApiResult<string>> UpdateAccountInfo(AccountForUpdateDto updateDto,string accountId)
        {
            var checkAccount = await _context.AppUsers.Where(x => x.Id.ToString() == accountId).SingleOrDefaultAsync();
            if(checkAccount==null) return new ApiResult<string>(HttpStatusCode.NotFound,$"Không tìm thấy tài khoản");
            var checkCustomer =
                await _context.Customers.Where(x => x.AppUserId == checkAccount.Id).SingleOrDefaultAsync();
            if (checkCustomer != null)
            {
                checkCustomer.Name = updateDto.Name;
                checkCustomer.Gender = updateDto.Gender;
                checkCustomer.Dob = updateDto.Dob;
                checkCustomer.Address = updateDto.Address;
                checkCustomer.PhoneNumber = updateDto.PhoneNumber;
                checkAccount.PhoneNumber = updateDto.PhoneNumber;
                checkCustomer.Website = updateDto.Website;
                checkCustomer.Fax = updateDto.Fax;
                await _context.SaveChangesAsync();
                return new ApiResult<string>(HttpStatusCode.OK)
                {
                    ResultObj = accountId,
                    Message = "Cập nhập thông tin tài khoản thành công"
                };
            }
            return new ApiResult<string>(HttpStatusCode.BadRequest,"Người dùng liên kết với tài khoản không được phép chỉnh sửa thông tin, hãy liên hệ với quản trị viên để cập nhập thông tin cá nhân");
        }

        public async Task<ApiResult<PagedResult<AccountOrdersDto>>> GetAccountOrders(int pageIndex, int pageSize, string accountId)
        {
            var checkCustomer = await _context.Customers.Where(x => x.AppUserId.ToString() == accountId)
                .SingleOrDefaultAsync();
            if(checkCustomer==null) return new ApiResult<PagedResult<AccountOrdersDto>>(HttpStatusCode.NotFound,$"Không tìm thấy khách hàng");
            var orders = await (from o in _context.Orders
                join ts in _context.TransactionStatuses on o.TransactionStatusId equals ts.Id
                orderby o.DateCreated descending 
                where o.CustomerId == checkCustomer.Id
                select new AccountOrdersDto()
                {
                    OrderId = o.Id,
                    DateCreated = o.DateCreated,
                    TotalAmount = o.TotalAmount,
                    TransactionStatus = ts.Name
                }).GetPagedAsync(pageIndex, pageSize);
            return new ApiResult<PagedResult<AccountOrdersDto>>(HttpStatusCode.OK,orders);
        }

        public async Task<ApiResult<string>> CancelOrder(string orderId, string accountId)
        {
            var checkCustomer = await _context.Customers.Where(x => x.AppUserId.ToString() == accountId)
                .SingleOrDefaultAsync();
            if(checkCustomer==null) return new ApiResult<string>(HttpStatusCode.NotFound,$"Không tìm thấy khách hàng");
            var checkOrder = await _context.Orders.Where(x => x.Id == orderId).SingleOrDefaultAsync();
            if(checkOrder==null) return new ApiResult<string>(HttpStatusCode.NotFound,$"Không tìm thấy đơn hàng có mã: {orderId}");
            if(checkOrder.TransactionStatusId!=GlobalProperties.WaitingTransactionId) return new ApiResult<string>(HttpStatusCode.BadRequest,$"Chỉ được phép hủy đơn hàng đang trong trạng thái chờ");
            checkOrder.TransactionStatusId = GlobalProperties.CancelTransactionId;
            await _context.SaveChangesAsync();
            return new ApiResult<string>(HttpStatusCode.OK)
            {
                ResultObj = orderId,
                Message = "Hủy đơn hàng thành công"
            };
        }
    }
}