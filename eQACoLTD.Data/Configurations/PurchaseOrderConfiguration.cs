using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.ToTable("PurchaseOrders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("varchar(12)");
            builder.Property(x => x.Note).HasColumnType("nvarchar(250)");
            builder.Property(x => x.DateCreated).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.PurchaseDate).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
            builder.Property(x => x.DiscountTypeId).IsRequired(false).HasDefaultValue();
            builder.Property(x => x.DiscountValue).HasColumnType("decimal").HasDefaultValue(0);
            builder.Property(x => x.DiscountDescription).HasColumnType("nvarchar(500)");

            builder.HasOne(s => s.Supplier)
                .WithMany(p => p.PurchaseOrders)
                .HasForeignKey(p => p.SupplierId);
            builder.HasOne(o => o.OrderStatus)
                .WithMany(p => p.PurchaseOrders)
                .HasForeignKey(p => p.OrderStatusId);
            builder.HasOne(pa => pa.PaymentStatus)
                .WithMany(p => p.PurchaseOrders)
                .HasForeignKey(p => p.PaymentStatusId);
            builder.HasOne(dt => dt.DiscountType)
                .WithMany(pu => pu.PurchaseOrders)
                .HasForeignKey(pu => pu.DiscountTypeId);
        }
    }
}
