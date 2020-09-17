using eQACoLTD.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace eQACoLTD.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seeding(this ModelBuilder modelBuilder)
        {
            #region Roles
            var roleWarehouseStaff = new AppRole()
            {
                Id = Guid.NewGuid(),
                Name = "WarehouseStaff",
                NormalizedName = "WarehouseStaff",
                Description = "Quyền nhân viên kho"
            };
            var roleReceptionists = new AppRole()
            {
                Id = Guid.NewGuid(),
                Name = "Receptionist",
                NormalizedName = "Receptionist",
                Description = "Quyền nhân viên lễ tân"
            };
            var roleCashier = new AppRole()
            {
                Id = Guid.NewGuid(),
                Name = "Cashier",
                NormalizedName = "Cashier",
                Description = "Quyền nhân viên thu ngân"
            };
            var rolesAdmin = new AppRole()
            {
                Id = Guid.NewGuid(),
                Name = "Administrator",
                NormalizedName = "Administrator",
                Description = "Quyền quản trị viên"
            };

            modelBuilder.Entity<AppRole>().HasData(rolesAdmin, roleWarehouseStaff, roleReceptionists, roleCashier);
            #endregion

            #region AppUsers
            var hasher = new PasswordHasher<AppUser>();
            var userAdmin = new AppUser()
            {
                Id = Guid.NewGuid(),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "dnguyen24498@gmail.com",
                NormalizedEmail = "DNGUYEN24498@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Bonghoatrang1@!"),
                SecurityStamp = string.Empty
            };
            var userEmployee = new AppUser()
            {
                Id = Guid.NewGuid(),
                UserName = "epn0001",
                NormalizedUserName = "EPN0001",
                Email = "duongnguyenadhp1@gmail.com",
                NormalizedEmail = "DUONGNGUYENADHP1@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Bonghoatrang1@!"),
                SecurityStamp = string.Empty,
                PhoneNumber = "1234567890"
            };
            var userEmployee2 = new AppUser()
            {
                Id = Guid.NewGuid(),
                UserName = "epn0002",
                NormalizedUserName = "epn0002",
                Email = "duongnguyenadhp2@gmail.com",
                NormalizedEmail = "DUONGNGUYENADHP2@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Bonghoatrang1@!"),
                SecurityStamp = string.Empty,
                PhoneNumber = "1234567890"
            };
            var userCustomer = new AppUser()
            {
                Id = Guid.NewGuid(),
                UserName = "cus0001",
                NormalizedUserName = "cus0001",
                Email = "duongnguyenadhp@gmail.com",
                NormalizedEmail = "DUONGNGUYENADHP@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Bonghoatrang1@!"),
                PhoneNumber = "1234567890",
                SecurityStamp = string.Empty
            };
            var userCustomer2 = new AppUser()
            {
                Id = Guid.NewGuid(),
                UserName = "cus0002",
                NormalizedUserName = "cus0002",
                Email = "dophuongthao@gmail.com",
                NormalizedEmail = "DOPHUONGTHAO@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Bonghoatrang1@!"),
                PhoneNumber = "1234567890",
                SecurityStamp=string.Empty
            };

            modelBuilder.Entity<AppUser>().HasData(userAdmin, userEmployee,userEmployee2, userCustomer,userCustomer2);

            #endregion

            #region AppUserRoles
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
                new IdentityUserRole<Guid>
                {
                    RoleId = rolesAdmin.Id,
                    UserId = userAdmin.Id
                },
                new IdentityUserRole<Guid>
                {
                    RoleId = roleCashier.Id,
                    UserId = userEmployee.Id
                },
                new IdentityUserRole<Guid>
                {
                    RoleId = roleWarehouseStaff.Id,
                    UserId = userEmployee2.Id
                }
            );
            #endregion

            #region CustomerTypes
            var retailCustomer = new CustomerType()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Bán lẻ"
            };
            var wholeSalesCustomer = new CustomerType()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Bán buôn"
            };

            modelBuilder.Entity<CustomerType>().HasData(retailCustomer, wholeSalesCustomer);
            #endregion

            #region Employee
            var employee1 = new Employee()
            {
                Id = "EPN0001",
                Dob = new DateTime(1998, 04, 24),
                FullName="Nguyễn Dương Nguyên",
                Address = "Số 88, Hải Triều, Số 99, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng",
                Gender = true,
                DefaultPhoneNumber = "0123456789",
                UserId = userEmployee.Id,
            };
            var employee2 = new Employee()
            {
                Id = "EPN0002",
                Dob = new DateTime(1998, 04, 24),
                FullName="Bùi Thùy Dương",
                Address = "Số 99, Hải Triều, Số 99, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng",
                Gender = false,
                DefaultPhoneNumber = "0123456789",
                UserId = userEmployee2.Id,
            };
            modelBuilder.Entity<Employee>().HasData(employee1,employee2);
            #endregion

            #region Customers
            var customer1 = new Customer()
            {
                Id = "CUS0001",
                Dob = new DateTime(1998, 04, 24),
                FullName="Bùi Thùy Dương",
                Address = "Số 99, Hải Triều, Số 99, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng",
                Gender = false,
                UserId = userCustomer.Id,
                CustomerTypeId = retailCustomer.Id,
            };
            var customer2 = new Customer()
            {
                Id = "CUS0002",
                Dob = new DateTime(1998, 04, 24),
                FullName="Đỗ Phương Thảo",
                Address = "Số 99, Hải Triều, Số 99, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng",
                Gender = false,
                UserId = userCustomer2.Id,
                CustomerTypeId = retailCustomer.Id,
            };
            modelBuilder.Entity<Customer>().HasData(customer1,customer2);
            #endregion

            #region Suppliers
            var supplier1 = new Supplier()
            {
                Id = "SUN0001",
                Name = "Công ty TNHH ABC",
                Address = "Số 99, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng",
                PhoneNumber = "1234567890",
                Email = "contyabc@gmail.com",
                Fax = "1234567890",
                EmployeeId = employee1.Id,
                Website = "abc.com.vn"
            };
            var supplier2 = new Supplier()
            {
                Id = "SUN0002",
                Name = "Công ty TNHH DEF",
                Address = "Số 99, Hải Triều, Quán Toan, Hồng Bàng, Hải Phòng",
                PhoneNumber = "1234567890",
                Email = "contyabc@gmail.com",
                Fax = "1234567890",
                EmployeeId = employee1.Id,
                Website = "abc.com.vn"
            };
            modelBuilder.Entity<Supplier>().HasData(supplier1,supplier2);
            #endregion

            #region Categories
            var laptopCategory = new Category()
            {
                Id = "3db32842-3ed5-458b-ad96-a113d73775ee",
                Name = "Laptop"
            };
            var pcCategory = new Category()
            {
                Id = "58060083-6e76-4dea-ba20-0e332ec96122",
                Name = "PC"
            };
            modelBuilder.Entity<Category>().HasData(laptopCategory, pcCategory);
            #endregion

            #region Brands
            var appleBrand = new Brand()
            {
                Id = "bedf848d-f7d0-433f-98f1-2731067335af",
                Name = "Apple"
            };
            var dellBrand = new Brand()
            {
                Id = "b693eb5a-e457-447c-9e29-48f438703780",
                Name = "Dell"
            };
            var hpBrand = new Brand()
            {
                Id = "9d07f19f-afd8-408d-872e-d99331031c59",
                Name = "HP"
            };
            modelBuilder.Entity<Brand>().HasData(appleBrand, dellBrand, hpBrand);
            #endregion

            #region Products
            var macbookProProduct = new Product()
            {
                Id = "PRN0001",
                Name = "Macbook Pro 2020",
                CategoryId = laptopCategory.Id,
                Views = 1340,
                StarScore = 1,
                RetailPrice = 22500000,
                WholesalePrices = 21500000,
                WarrantyPeriod = 36,
                BrandId=appleBrand.Id
            };
            var macbookAirProduct = new Product()
            {
                Id = "PRN0002",
                Name = "Macbook Air 2020",
                CategoryId = laptopCategory.Id,
                Views = 1340,
                StarScore = 1,
                RetailPrice = 20500000,
                WholesalePrices = 20000000,
                WarrantyPeriod = 36,
                BrandId = appleBrand.Id
            };
            modelBuilder.Entity<Product>().HasData(macbookProProduct, macbookAirProduct);
            #endregion

            #region PaymentStatuses
            var unpaidPayment = new PaymentStatus()
            {
                Id = "57fa4d1a-974a-4157-bd6c-287db184a51e",
                Name = "Chưa thanh toán"
            };
            var partialPayment = new PaymentStatus()
            {
                Id = "6187b25a-957c-4690-ba99-f64499119f35",
                Name = "Thanh toán một phần"
            };
            var paidPayment = new PaymentStatus()
            {
                Id = "646233ce-0eb0-4ed1-a5c6-8e16e1376393",
                Name = "Đã thanh toán"
            };
            modelBuilder.Entity<PaymentStatus>().HasData(unpaidPayment, partialPayment, paidPayment);
            #endregion

            #region PaymentMethods
            var cashPayment = new PaymentMethod()
            {
                Id = "a2b931dd-c5be-47c5-a134-f9f3474cf630",
                Name = "Tiền mặt"
            };
            var cardPayment = new PaymentMethod()
            {
                Id = "2107adeb-7574-4f62-a898-22b79b67ee62",
                Name = "Quẹt thẻ"
            };
            var bankingPayment = new PaymentMethod()
            {
                Id = "f859d3ef-13e6-411b-b701-d5d643524653",
                Name = "Chuyển khoản"
            };

            modelBuilder.Entity<PaymentMethod>().HasData(cashPayment, cardPayment, bankingPayment);
            #endregion

            #region OrderStatuses
            var orderingStatus = new OrderStatus()
            {
                Id = "75d76ab9-5e0e-4b42-8bdc-7e3c5d4904a1",
                Name = "Đặt hàng"
            };
            var tradingStatus = new OrderStatus()
            {
                Id = "43972588-ea79-4bf0-8ed7-7e9ef42170a7",
                Name = "Đang giao dịch"
            };
            var finishedStatus = new OrderStatus()
            {
                Id = "80b1a453-82a4-47ed-964e-1b69af87a1e1",
                Name = "Hoàn thành"
            };
            var endStatus = new OrderStatus()
            {
                Id = "537d8259-b206-496a-8a85-29f76539301b",
                Name = "Kết thúc"
            };
            var cancelStatus = new OrderStatus()
            {
                Id = "940195d2-885c-45aa-af98-17ad7549f90a",
                Name = "Đã hủy"
            };

            modelBuilder.Entity<OrderStatus>().HasData(orderingStatus, tradingStatus, finishedStatus, endStatus, cancelStatus);
            #endregion

            #region StockActions
            var initialStock = new StockAction()
            {
                Id = "d6f2e2be-d9d4-40bf-b872-1c5d747466c9",
                Name = "Khởi tạo"
            };
            var deliveryStock = new StockAction()
            {
                Id = "1be88fef-0b15-4429-9012-9771a6e849f1",
                Name = "Xuất kho giao hàng cho khách/shipper"
            };
            var receiptStock = new StockAction()
            {
                Id = "0a12a679-0952-414f-a1e6-990b34f1be07",
                Name = "Nhập hàng vào kho"
            };
            var adjustmentStock = new StockAction()
            {
                Id = "a8ef7eae-3fad-4711-a4f0-8b5b7e5084af",
                Name = "Cân bằng kho"
            };

            modelBuilder.Entity<StockAction>().HasData(initialStock, deliveryStock, receiptStock, adjustmentStock);
            #endregion

            #region Transporters
            var ghnTransporter = new Transporter()
            {
                Id = "be2ba488-7bf5-491d-ac72-15d3d4c50c05",
                Name = "Giao hàng nhanh",
                PhoneNumber = "1234567890"
            };

            modelBuilder.Entity<Transporter>().HasData(ghnTransporter);
            #endregion

            #region PurchaseOrders
            var purchaseOrder1 = new PurchaseOrder()
            {
                Id = "PON0001",
                SupplierId = supplier1.Id,
                OrderStatusId = finishedStatus.Id,
                DeliveryDate = DateTime.Now,
                PaymentStatusId = paidPayment.Id
            };
            var purchaseOrder2 = new PurchaseOrder()
            {
                Id = "PON0002",
                SupplierId = supplier2.Id,
                OrderStatusId = tradingStatus.Id,
                DeliveryDate = DateTime.Now,
                PaymentStatusId = partialPayment.Id
            };
            modelBuilder.Entity<PurchaseOrder>().HasData(purchaseOrder1,purchaseOrder2);
            #endregion

            #region PurchaseOrderDetails
            var purchaseOrderDetail1 = new PurchaseOrderDetail()
            {
                Id=Guid.NewGuid().ToString(),
                PurchaseOrderId = purchaseOrder1.Id,
                ProductId = macbookProProduct.Id,
                Quantity = 100,
                UnitPrice = 22500000
            };
            var purchaseOrderDetail2 = new PurchaseOrderDetail()
            {
                Id = Guid.NewGuid().ToString(),
                PurchaseOrderId = purchaseOrder2.Id,
                ProductId = macbookAirProduct.Id,
                Quantity = 50,
                UnitPrice = 22500000
            };
            modelBuilder.Entity<PurchaseOrderDetail>().HasData(purchaseOrderDetail1,purchaseOrderDetail2);
            #endregion

            #region PaymentVouchers
            var paymentVoucher1 = new PaymentVoucher()
            {
                Id = "PVN0001",
                PurchaseOrderId = purchaseOrder1.Id,
                Paid = 2250000000,
                PayDate = DateTime.Now,
                PaymentMethodId = cashPayment.Id
            };
            var paymentVoucher2 = new PaymentVoucher()
            {
                Id = "PVN0002",
                PurchaseOrderId = purchaseOrder2.Id,
                Paid = 500000000,
                PayDate = DateTime.Now,
                PaymentMethodId = cashPayment.Id
            };
            modelBuilder.Entity<PaymentVoucher>().HasData(paymentVoucher1,paymentVoucher2);
            #endregion

            #region Stocks
            var stock = new Stock()
            {
                ProductId = macbookProProduct.Id,
                AbleToSale = 99,
                Inventory = 99
            };
            var stock2 = new Stock()
            {
                ProductId = macbookAirProduct.Id,
                AbleToSale = 49,
                Inventory=49
            };
            modelBuilder.Entity<Stock>().HasData(stock,stock2);
            #endregion

            #region Orders
            var order1 = new Order()
            {
                Id = "ODN0001",
                CustomerId = customer1.Id,
                OrderStatusId = finishedStatus.Id,
                PaymentStatusId = paidPayment.Id
            };
            var order2 = new Order()
            {
                Id = "ODN0002",
                CustomerId = customer2.Id,
                OrderStatusId = tradingStatus.Id,
                PaymentStatusId = partialPayment.Id
            };
            modelBuilder.Entity<Order>().HasData(order1,order2);
            #endregion

            #region OrderDetails
            var orderDetail1 = new OrderDetail()
            {
                Id=Guid.NewGuid().ToString(),
                OrderId = order1.Id,
                ProductId = macbookProProduct.Id,
                Quantity = 1,
                UnitPrice = 22500000
            };
            var orderDetail2 = new OrderDetail()
            {
                Id=Guid.NewGuid().ToString(),
                OrderId = order2.Id,
                ProductId = macbookAirProduct.Id,
                Quantity = 1,
                UnitPrice = 22500000
            };
            modelBuilder.Entity<OrderDetail>().HasData(orderDetail1,orderDetail2);
            #endregion

            #region ReceiptVouchers
            var receiptVoucher1 = new ReceiptVoucher()
            {
                Id = "RVN0001",
                OrderId = order1.Id,
                Received = 22500000,
                ReceivedDate = DateTime.Now,
                PaymentMethodId = cashPayment.Id
            };
            var receiptVoucher2 = new ReceiptVoucher()
            {
                Id = "RVN0002",
                OrderId = order2.Id,
                Received = 10500000,
                ReceivedDate = DateTime.Now,
                PaymentMethodId = cashPayment.Id
            };
            modelBuilder.Entity<ReceiptVoucher>().HasData(receiptVoucher1,receiptVoucher2);
            #endregion

            #region StockHistories
            modelBuilder.Entity<StockHistory>().HasData(
                new StockHistory()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = macbookProProduct.Id,
                    RecordDate = DateTime.Now,
                    EmployeeId = employee1.Id,
                    StockActionId = initialStock.Id,
                    ChangeQuantity = 100,
                    PurchaseOrderDetailId = purchaseOrderDetail1.Id
                },
                new StockHistory()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = macbookAirProduct.Id,
                    RecordDate = DateTime.Now,
                    EmployeeId = employee1.Id,
                    StockActionId = initialStock.Id,
                    ChangeQuantity = 50,
                    PurchaseOrderDetailId = purchaseOrderDetail2.Id
                },
                new StockHistory()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = macbookProProduct.Id,
                    RecordDate = DateTime.Now,
                    EmployeeId = employee1.Id,
                    StockActionId = deliveryStock.Id,
                    ChangeQuantity = -1,
                    OrderDetailId = orderDetail1.Id
                },
                 new StockHistory()
                 {
                     Id = Guid.NewGuid().ToString(),
                     ProductId = macbookAirProduct.Id,
                     RecordDate = DateTime.Now,
                     EmployeeId = employee1.Id,
                     StockActionId = deliveryStock.Id,
                     ChangeQuantity = -1,
                     OrderDetailId = orderDetail2.Id
                 }
            );
            #endregion

            #region DiscountTypes
            modelBuilder.Entity<DiscountType>().HasData(
                new DiscountType() { 
                    Id="0d3c8b87-8010-491b-9867-84805888d937",
                    Name="Phần trăm"
                },
                new DiscountType() { 
                    Id="35d3f8e6-4c28-4315-8e98-5ea957c847c2",
                    Name="Tiền mặt"
                });
            #endregion


        }
    }
}
