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
            builder.Property(x => x.UnitPrice).HasColumnType("decimal").IsRequired();
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
                },
                new PurchaseOrderDetail()
                {
                    Id = "ee02d34b-9b06-4ff4-bd7c-4fd77f93fd0b",
                    PurchaseOrderId = "PON0001",
                    ProductId = "PRN0003",
                    UnitPrice = 66000000,
                    Quantity = 10
                },
                
                new PurchaseOrderDetail()
                {
                    Id = "48248861-6679-4f7a-b4d7-a03d45dffec0",
                    PurchaseOrderId = "PON0002",
                    ProductId = "PRN0004",
                    UnitPrice = 27590000,
                    Quantity = 10
                },
                new PurchaseOrderDetail()
                {
                    Id = "3f66e689-4be2-446d-ae95-6ae72a4f9e76",
                    PurchaseOrderId = "PON0002",
                    ProductId = "PRN0005",
                    UnitPrice = 31990000,
                    Quantity = 10
                },
                new PurchaseOrderDetail()
                {
                    Id = "f835b3e4-9161-4b55-893e-6437c32eb912",
                    PurchaseOrderId = "PON0002",
                    ProductId = "PRN0006",
                    UnitPrice = 13990000,
                    Quantity = 10
                },
                
                new PurchaseOrderDetail()
                {
                    Id = "90b1fca1-24e9-4a42-ba51-c4f1345e997c",
                    PurchaseOrderId = "PON0003",
                    ProductId = "PRN0007",
                    UnitPrice = 9990000,
                    Quantity = 10
                },
                new PurchaseOrderDetail()
                {
                    Id = "e7247dd5-af71-4599-b83c-9617ed666418",
                    PurchaseOrderId = "PON0003",
                    ProductId = "PRN0008",
                    UnitPrice = 2990000,
                    Quantity = 10
                },
                new PurchaseOrderDetail()
                {
                    Id = "32f2fe5b-4646-48f5-a6e9-e24d8c3937f5",
                    PurchaseOrderId = "PON0003",
                    ProductId = "PRN0010",
                    UnitPrice = 280000,
                    Quantity = 10
                },
                new PurchaseOrderDetail()
                {
                    Id = "a2fea2b7-5d1d-4061-b9af-d7aae0076478",
                    PurchaseOrderId = "PON0003",
                    ProductId = "PRN0011",
                    UnitPrice = 280000,
                    Quantity = 10
                },
                new PurchaseOrderDetail()
                {
                    Id = "54e021fa-0dc8-4e07-b2a8-d4017caa450a",
                    PurchaseOrderId = "PON0003",
                    ProductId = "PRN0016",
                    UnitPrice = 22990000,
                    Quantity = 1
                });
        }
    }
}
