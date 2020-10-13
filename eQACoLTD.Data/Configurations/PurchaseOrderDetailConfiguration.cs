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

            builder.HasData(
                new PurchaseOrderDetail()
                {
                    Id = "73e88c7b-6a11-4c27-9045-308d2d1b553a",
                    PurchaseOrderId = "PON0001",
                    ProductId = "PRN0001",
                    UnitPrice = 42000000,
                    Quantity = 10
                },
                new PurchaseOrderDetail()
                {
                    Id = "e93c0a07-69a3-4736-9671-21ce451bc656",
                    PurchaseOrderId = "PON0001",
                    ProductId = "PRN0002",
                    UnitPrice = 30000000,
                    Quantity = 10
                });
        }
    }
}
