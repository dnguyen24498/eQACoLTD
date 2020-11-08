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

        public async Task<ApiResult<Guid>> AddRoleAsync(Guid userId, Guid roleId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString("D"));
            if (user == null) return new ApiResult<Guid>(HttpStatusCode.NotFound,$"Không tìm thấy tài khoản có mã: {userId}");
            var role = await _roleManager.FindByIdAsync(roleId.ToString("D"));
            if(role==null) return new ApiResult<Guid>(HttpStatusCode.NotFound,$"Không tìm thấy quyền có mã: {roleId}");
            var isInRole = await _userManager.IsInRoleAsync(user, role.Name);
            if(isInRole) return new ApiResult<Guid>(HttpStatusCode.NotModified,"Tài khoản đã có quyền này");
            var result=await _userManager.AddToRoleAsync(user, role.Name);
            if(result.Succeeded)
                return new ApiResult<Guid>(HttpStatusCode.OK,roleId)
                {
                    Message = "Đã thêm quyền"
                };
            return new ApiResult<Guid>(HttpStatusCode.NotModified,$"Có lỗi khi thêm quyền");
        }

        public async Task<ApiResult<Guid>> RemoveRoleAsync(Guid userId, Guid roleId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString("D"));
            if (user == null) return new ApiResult<Guid>(HttpStatusCode.NotFound,$"Không tìm thấy tài khoản có mã: {userId}");
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
                    UnitPrice = p.RetailPrice,
                    ImagePath = p.ProductImages.Where(x=>x.IsThumbnail==true).SingleOrDefault().Path
                }).ToListAsync();
            var cart=new CartDto()
            {
                CustomerId = checkCustomer.Id,
                ListProduct = cartDetails,
                TotalAmount = cartDetails==null||cartDetails.Count==0?0:cartDetails.Sum(x => x.Quantity * x.UnitPrice)
            };
            return new ApiResult<CartDto>(HttpStatusCode.OK,cart);
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

        public async Task<ApiResult<CustomerInfo>> GetCurrentCustomerInfo(string customerId)
        {
            var checkCustomerAccount = await _context.AppUsers.Where(x => x.Id.ToString() == customerId).SingleOrDefaultAsync();
            if (checkCustomerAccount == null) return new ApiResult<CustomerInfo>(HttpStatusCode.NotFound, $"Không tìm thấy tài khoản");
            var checkCustomer = await _context.Customers.Where(x => x.AppUserId == checkCustomerAccount.Id).SingleOrDefaultAsync();
            if (checkCustomer == null) return new ApiResult<CustomerInfo>(HttpStatusCode.NotFound, $"Không tìm thấy khách hàng");
            var customer = await (from c in _context.Customers
                join au in _context.AppUsers on c.AppUserId equals au.Id
                where c.Id==checkCustomer.Id && c.IsDelete==false    
                select new CustomerInfo()
                {
                    Address = c.Address,
                    Name = c.Name,
                    PhoneNumber = c.PhoneNumber ?? au.PhoneNumber
                }).SingleOrDefaultAsync();
            return new ApiResult<CustomerInfo>(HttpStatusCode.OK,customer);
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
                    UnitPrice =  await (from p in _context.Products where p.Id==cart.ProductId select p.RetailPrice).SingleOrDefaultAsync()
                });
            }
            var order=new Data.Entities.Order()
            {
                Id = orderId,
                CustomerId = checkCustomer.Id,
                TransactionStatusId = GlobalProperties.WaitingTransactionId,
                PaymentStatusId = GlobalProperties.UnpaidPaymentId,
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
    }
}