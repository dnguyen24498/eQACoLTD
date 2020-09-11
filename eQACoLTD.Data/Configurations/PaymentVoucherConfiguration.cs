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
            builder.Property(x => x.PayDate).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
            builder.Property(x => x.Description).HasColumnType("nvarchar(250)");

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
        }
    }
}
