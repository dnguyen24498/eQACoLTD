using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class ReceiptVoucherConfiguration : IEntityTypeConfiguration<ReceiptVoucher>
    {
        public void Configure(EntityTypeBuilder<ReceiptVoucher> builder)
        {
            builder.ToTable("ReceiptVouchers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("varchar(12)");
            builder.Property(x => x.Received).HasColumnType("decimal").IsRequired().HasDefaultValue(0);
            builder.Property(x => x.ReceivedDate).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
            builder.Property(x => x.SupplierId).IsRequired(false);
            builder.Property(x => x.CustomerId).IsRequired(false);
            builder.Property(x => x.Description).HasColumnType("nvarchar(250)");
            builder.Property(x => x.DateCreated).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            
            
            builder.HasOne(pm => pm.PaymentMethod)
               .WithMany(rv => rv.ReceiptVouchers)
               .HasForeignKey(rv => rv.PaymentMethodId);
            builder.HasOne(s => s.Supplier)
               .WithMany(rv => rv.ReceiptVouchers)
               .HasForeignKey(rv => rv.SupplierId);
            builder.HasOne(c => c.Customer)
               .WithMany(rv => rv.ReceiptVouchers)
               .HasForeignKey(rv => rv.CustomerId);
            builder.HasOne(e => e.Employee)
                .WithMany(rv => rv.ReceiptVouchers)
                .HasForeignKey(rv => rv.EmployeeId);
            builder.HasOne(b => b.Branch)
                .WithMany(rv => rv.ReceiptVouchers)
                .HasForeignKey(rv => rv.BranchId);
            builder.HasOne(r => r.RepairVoucher)
                .WithMany(rv => rv.ReceiptVouchers)
                .HasForeignKey(rv => rv.RepairVoucherId);
            builder.HasOne(l => l.LiquidationVoucher)
                .WithMany(rv => rv.ReceiptVouchers)
                .HasForeignKey(rv => rv.LiquidationVoucherId);
            builder.HasOne(r => r.Return)
                .WithMany(rv => rv.ReceiptVouchers)
                .HasForeignKey(rv => rv.ReturnId);
            builder.HasOne(s => s.Shipping)
                .WithMany(rv => rv.ReceiptVouchers)
                .HasForeignKey(rv => rv.ShippingId);
            builder.HasOne(s => s.Order)
                .WithMany(r => r.ReceiptVouchers)
                .HasForeignKey(r => r.OrderId);

            builder.HasData(
                new ReceiptVoucher()
                {
                    Id="RVN0001",
                    OrderId = "SRN0001",
                    Received = 45000000,
                    PaymentMethodId = "7cd60e3f-c215-42b3-a98e-c4ac4fe71b63",
                    IsDelete = false,
                    CustomerId = "CUS0001",
                    EmployeeId="EPN0001",
                    BranchId = "ec4c314e-90b1-464c-aa52-2d34e555875e"
                });


        }
    }
}
