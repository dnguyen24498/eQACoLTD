using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class PaymentVoucherConfiguration : IEntityTypeConfiguration<PaymentVoucher>
    {
        public void Configure(EntityTypeBuilder<PaymentVoucher> builder)
        {
            builder.ToTable("PaymentVouchers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("varchar(12)");
            builder.Property(x => x.Paid).IsRequired();
            builder.Property(x => x.PaymentDate).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
            builder.Property(x => x.Description).HasColumnType("nvarchar(250)");
            builder.Property(x => x.DateCreated).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            

            builder.HasOne(po => po.PurchaseOrder)
                .WithMany(pv => pv.PaymentVouchers)
                .HasForeignKey(pv => pv.PurchaseOrderId);
            builder.HasOne(p => p.PaymentMethod)
                .WithMany(pv => pv.PaymentVouchers)
                .HasForeignKey(pv => pv.PaymentMethodId);
            builder.HasOne(c => c.Customer)
                .WithMany(pv => pv.PaymentVouchers)
                .HasForeignKey(pv => pv.CustomerId);
            builder.HasOne(s => s.Supplier)
                .WithMany(pv => pv.PaymentVouchers)
                .HasForeignKey(pv => pv.SupplierId);
            builder.HasOne(x => x.Employee)
                .WithMany(p => p.PaymentVouchers)
                .HasForeignKey(p => p.EmployeeId);
            builder.HasOne(b => b.Branch)
                .WithMany(p => p.PaymentVouchers)
                .HasForeignKey(p => p.BranchId);
            builder.HasOne(r => r.Return)
                .WithMany(p => p.PaymentVouchers)
                .HasForeignKey(p => p.ReturnId);
            builder.HasOne(p => p.Shipping)
                .WithMany(p => p.PaymentVouchers)
                .HasForeignKey(p => p.ShippingId);

            builder.HasData(
                new PaymentVoucher()
                {
                    Id = "PVN0001",
                    PurchaseOrderId = "PON0001",
                    Paid = 320000000,
                    DateCreated = DateTime.Now,
                    PaymentDate = DateTime.Now,
                    IsDelete = false,
                    PaymentMethodId = "7cd60e3f-c215-42b3-a98e-c4ac4fe71b63",
                    BranchId = "ec4c314e-90b1-464c-aa52-2d34e555875e",
                });
        }
    }
}
