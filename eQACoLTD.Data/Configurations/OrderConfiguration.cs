using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("varchar(12)");
            builder.Property(x => x.DateCreated).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Note).HasColumnType("nvarchar(250)");
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
            builder.Property(x => x.DiscountTypeId).IsRequired(false).HasDefaultValue();

            builder.HasOne(c => c.Customer)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.CustomerId);
            builder.HasOne(os => os.OrderStatus)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.OrderStatusId);
            builder.HasOne(p => p.PaymentStatus)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.PaymentStatusId);
            builder.HasOne(dt => dt.DiscountType)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.DiscountTypeId);
        }
    }
}
