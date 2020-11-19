using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stocks");
            builder.HasKey(x => x.ProductId);
            builder.Property(x => x.RealQuantity).HasColumnType("int");
            builder.Property(x => x.AbleToSale).HasColumnType("int");
            builder.Property(x => x.PlacedLocation).HasColumnType("nvarchar(500)");

            builder.HasOne(p => p.Product)
                .WithMany(s => s.Stocks)
                .HasForeignKey(s => s.ProductId);
            builder.HasOne(w => w.Warehouse)
                .WithMany(s => s.Stocks)
                .HasForeignKey(s => s.WarehouseId);

            builder.HasData(
                new Stock()
                {
                    ProductId = "PRN0001",
                    WarehouseId = "d6bbee65-fe3d-4765-b569-202d9f3aa4f5",
                    RealQuantity = 9,
                    AbleToSale = 9,
                },
                new Stock()
                {
                    ProductId = "PRN0002",
                    WarehouseId = "d6bbee65-fe3d-4765-b569-202d9f3aa4f5",
                    RealQuantity = 10,
                    AbleToSale = 10,
                },
                new Stock()
                {
                    ProductId = "PRN0003",
                    WarehouseId = "d6bbee65-fe3d-4765-b569-202d9f3aa4f5",
                    RealQuantity = 10,
                    AbleToSale = 10,
                },
                new Stock()
                {
                    ProductId = "PRN0004",
                    WarehouseId = "d6bbee65-fe3d-4765-b569-202d9f3aa4f5",
                    RealQuantity = 10,
                    AbleToSale = 10,
                },
                new Stock()
                {
                    ProductId = "PRN0005",
                    WarehouseId = "d6bbee65-fe3d-4765-b569-202d9f3aa4f5",
                    RealQuantity = 10,
                    AbleToSale = 10,
                },
                new Stock()
                {
                    ProductId = "PRN0006",
                    WarehouseId = "d6bbee65-fe3d-4765-b569-202d9f3aa4f5",
                    RealQuantity = 10,
                    AbleToSale = 10,
                },
                new Stock()
                {
                    ProductId = "PRN0007",
                    WarehouseId = "d6bbee65-fe3d-4765-b569-202d9f3aa4f5",
                    RealQuantity = 10,
                    AbleToSale = 10,
                },
                new Stock()
                {
                    ProductId = "PRN0008",
                    WarehouseId = "d6bbee65-fe3d-4765-b569-202d9f3aa4f5",
                    RealQuantity = 10,
                    AbleToSale = 10,
                },
                new Stock()
                {
                    ProductId = "PRN0010",
                    WarehouseId = "d6bbee65-fe3d-4765-b569-202d9f3aa4f5",
                    RealQuantity = 10,
                    AbleToSale = 10,
                },
                new Stock()
                {
                    ProductId = "PRN0011",
                    WarehouseId = "d6bbee65-fe3d-4765-b569-202d9f3aa4f5",
                    RealQuantity = 10,
                    AbleToSale = 10,
                },
                new Stock()
                {
                    ProductId = "PRN0016",
                    WarehouseId = "d6bbee65-fe3d-4765-b569-202d9f3aa4f5",
                    RealQuantity = 1,
                    AbleToSale = 1,
                });
        }
    }
}
