using eQACoLTD.Data.Configurations;
using eQACoLTD.Data.Entities;
using eQACoLTD.Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.DBContext
{
    public class AppIdentityDbContext:IdentityDbContext<AppUser,AppRole,Guid>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new AppRoleConfiguration());
            builder.ApplyConfiguration(new CustomerTypeConfiguration());
            builder.ApplyConfiguration(new EmployeeConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new BrandConfiguration());
            builder.ApplyConfiguration(new StockActionConfiguration());
            builder.ApplyConfiguration(new OrderStatusConfiguration());
            builder.ApplyConfiguration(new PaymentStatusConfiguration());
            builder.ApplyConfiguration(new PaymentMethodConfiguration());
            builder.ApplyConfiguration(new TransporterConfiguration());
            builder.ApplyConfiguration(new ShippingStatusConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new CartConfiguration());
            builder.ApplyConfiguration(new SupplierConfiguration());
            builder.ApplyConfiguration(new ProductImageConfiguration());
            builder.ApplyConfiguration(new ProductReviewConfiguration());
            builder.ApplyConfiguration(new ProductReviewReplyConfiguration());
            builder.ApplyConfiguration(new PurchaseOrderConfiguration());
            builder.ApplyConfiguration(new PurchaseOrderDetailConfiguration());
            builder.ApplyConfiguration(new PaymentVoucherConfiguration());
            builder.ApplyConfiguration(new StockConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderDetailConfiguration());
            builder.ApplyConfiguration(new ReceiptVoucherConfiguration());
            builder.ApplyConfiguration(new StockHistoryConfiguration());
            builder.ApplyConfiguration(new ShippingOrderConfiguration());
            builder.ApplyConfiguration(new DiscountTypeConfiguration());

            builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => x.UserId);

            builder.Seeding();

        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<StockAction> StockActions { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Transporter> Transporters { get; set; }
        public DbSet<ShippingStatus> ShippingStatuses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ProductReviewReply> ProductReviewReplies { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public DbSet<PaymentVoucher> PaymentVouchers { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ReceiptVoucher> ReceiptVouchers { get; set; }
        public DbSet<StockHistory> StockHistories { get; set; }
        public DbSet<ShippingOrder> ShippingOrders { get; set; }
        public DbSet<DiscountType> DiscountTypes { get; set; }
    }
}
