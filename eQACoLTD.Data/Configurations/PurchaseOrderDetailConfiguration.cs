using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class PurchaseOrderDetailConfiguration : IEntityTypeConfiguration<PurchaseOrderDetail>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderDetail> builder)
        {
            builder.ToTable("PurchaseOrderDetails");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Quantity).HasDefaultValue(1);
            builder.Property(x => x.UnitPrice).IsRequired();
            builder.Property(x => x.CostName).HasColumnType("nvarchar(400)");

            builder.HasOne(po => po.PurchaseOrder)
                .WithMany(pod => pod.PurchaseOrderDetails)
                .HasForeignKey(pod => pod.PurchaseOrderId);
            builder.HasOne(p => p.Product)
                .WithMany(pod => pod.PurchaseOrderDetails)
                .HasForeignKey(pod => pod.ProductId);
        }
    }
}
