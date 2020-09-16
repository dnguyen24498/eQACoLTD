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
            builder.Property(x => x.Received).HasDefaultValue(0);
            builder.Property(x => x.ReceivedDate).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
            builder.Property(x => x.SupplierId).IsRequired(false);
            builder.Property(x => x.CustomerId).IsRequired(false);
            builder.Property(x => x.Description).HasColumnType("nvarchar(250)");

            builder.HasOne(o => o.Order)
                .WithMany(rv => rv.ReceiptVouchers)
                .HasForeignKey(rv => rv.OrderId);
            builder.HasOne(pm => pm.PaymentMethod)
               .WithMany(rv => rv.ReceiptVouchers)
               .HasForeignKey(rv => rv.PaymentMethodId);
            builder.HasOne(s => s.Supplier)
               .WithMany(rv => rv.ReceiptVouchers)
               .HasForeignKey(rv => rv.SupplierId);
            builder.HasOne(c => c.Customer)
               .WithMany(rv => rv.ReceiptVouchers)
               .HasForeignKey(rv => rv.CustomerId);
        }
    }
}
