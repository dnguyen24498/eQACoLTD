using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Quantity).HasDefaultValue(1);
            builder.Property(x => x.UnitPrice).IsRequired();
            builder.Property(x => x.ServiceName).HasColumnType("nvarchar(300)");

            builder.HasOne(o => o.Order)
                .WithMany(od => od.OrderDetails)
                .HasForeignKey(od => od.OrderId);
            builder.HasOne(p => p.Product)
                .WithMany(od => od.OrderDetails)
                .HasForeignKey(od => od.ProductId);
        }
    }
}
