using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("varchar(12)");
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(200)");
            builder.Property(x => x.OverView).HasColumnType("nvarchar(600)");
            builder.Property(x => x.Description).HasColumnType("nvarchar(max)");
            builder.Property(x => x.Views).HasDefaultValue(0);
            builder.Property(x => x.RetailPrice).HasDefaultValue(0);
            builder.Property(x => x.WholesalePrices).HasDefaultValue(0);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
            builder.Property(x => x.CategoryId).IsRequired(false).HasDefaultValue();
            builder.Property(x => x.BrandId).IsRequired(false).HasDefaultValue();
            builder.Property(x => x.Stars).HasColumnType("tinyint").HasDefaultValue(1);
            builder.Property(x => x.WarrantyPeriod).HasColumnType("tinyint").HasDefaultValue(0);
            builder.Property(x => x.MaximumQuantity).HasColumnType("int").HasDefaultValue(0);
            builder.Property(x => x.MinimumQuantity).HasColumnType("int").HasDefaultValue(0);

            builder.HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId);
            builder.HasOne(b => b.Brand)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.BrandId);

            builder.HasData(
                new Product()
                {
                    Id = "PRN0001",
                    Name = "Macbook Pro 13.3\" (2020)",
                    Views = 2100,
                    OverView = "Gray/I5-2.0GHz/16GB/512GB/TouchBar/New-Fullbox",
                    Stars = 1,
                    RetailPrice = 45000000,
                    WholesalePrices = 44500000,
                    BrandId = "85000231-9235-48a6-b852-c64bdcc3376b",
                    CategoryId = "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2",
                    WarrantyPeriod = 36,
                    IsDelete = false,
                    MinimumQuantity = 5,
                    MaximumQuantity = 100
                },
                new Product()
                {
                    Id = "PRN0002",
                    Name = "Macbook Pro 13.3\" (2019)",
                    Views = 2500,
                    OverView = "Gray/I5-1.4GHz/8GB/256GB/TouchBar/New-Fullbox",
                    Stars = 1,
                    RetailPrice = 32000000,
                    WholesalePrices = 31500000,
                    BrandId = "85000231-9235-48a6-b852-c64bdcc3376b",
                    CategoryId = "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2",
                    WarrantyPeriod = 36,
                    IsDelete = false,
                    MinimumQuantity = 5,
                    MaximumQuantity = 100
                },
                new Product()
                {
                    Id = "PRN0003",
                    Name = "iMac 27\" (2020)",
                    Views = 2930,
                    OverView = "I7-3.8GHz/8GB/1TB/Radeon Pro 5500 XT 8GB/New-Fullbox",
                    Stars = 1,
                    RetailPrice = 66800000,
                    WholesalePrices = 66500000,
                    BrandId = "85000231-9235-48a6-b852-c64bdcc3376b",
                    CategoryId = "c3eaeb51-38c5-4ac4-bbb1-9cbeb8881525",
                    WarrantyPeriod = 36,
                    IsDelete = false,
                    MinimumQuantity = 5,
                    MaximumQuantity = 100
                },
                new Product()
                {
                    Id = "PRN0004",
                    Name = "Mac Mini (2020)",
                    Views = 2930,
                    OverView = "I5-3.0GHz/8GB/512TB/Intel UHD Graphics 630/New-Fullbox",
                    Stars = 1,
                    RetailPrice = 27590000,
                    WholesalePrices = 27500000,
                    BrandId = "85000231-9235-48a6-b852-c64bdcc3376b",
                    CategoryId = "c3eaeb51-38c5-4ac4-bbb1-9cbeb8881525",
                    WarrantyPeriod = 36,
                    IsDelete = false,
                    MinimumQuantity = 5,
                    MaximumQuantity = 100
                },
                new Product()
                {
                    Id = "PRN0005",
                    Name = "Macbook Air 13.3\" (2020)",
                    Views = 2100,
                    OverView = "Gray/I5-1.1GHz/8GB/256GB/New-Fullbox",
                    Stars = 1,
                    RetailPrice = 31990000,
                    WholesalePrices = 31500000,
                    BrandId = "85000231-9235-48a6-b852-c64bdcc3376b",
                    CategoryId = "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2",
                    WarrantyPeriod = 36,
                    IsDelete = false,
                    MinimumQuantity = 5,
                    MaximumQuantity = 100
                },
                new Product()
                {
                    Id = "PRN0006",
                    Name = "Acer Swift 3 SF314",
                    Views = 2100,
                    OverView = "I3-2.1GHz/4GB/256GB/New-Fullbox",
                    Stars = 1,
                    RetailPrice = 13990000,
                    WholesalePrices = 13500000,
                    BrandId = "41f4bb29-5373-459a-8f6d-def64f15f747",
                    CategoryId = "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2",
                    WarrantyPeriod = 36,
                    IsDelete = false,
                    MinimumQuantity = 5,
                    MaximumQuantity = 100
                },
                new Product()
                {
                    Id = "PRN0007",
                    Name = "Acer Aspire 3 A315",
                    Views = 2100,
                    OverView = "I3-2.3GHz/4GB/1TB/New-Fullbox",
                    Stars = 1,
                    RetailPrice = 9990000,
                    WholesalePrices = 9500000,
                    BrandId = "41f4bb29-5373-459a-8f6d-def64f15f747",
                    CategoryId = "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2",
                    WarrantyPeriod = 36,
                    IsDelete = false,
                    MinimumQuantity = 5,
                    MaximumQuantity = 100
                },
                new Product()
                {
                    Id = "PRN0008",
                    Name = "Canon LBP 2900 Black",
                    Views = 2100,
                    OverView = "Chức năng: Print, Khổ giấy: A4/A5, In đảo mặt: Không, Cổng giao tiếp: USB, Dùng mực: Canon EP303",
                    Stars = 1,
                    RetailPrice = 2990000,
                    WholesalePrices = 2700000,
                    BrandId = "6c0d47b5-5b62-4c53-bd6e-680a3f08c1f0",
                    CategoryId = "610491a0-1824-4dd6-a8af-1f22f2a840a4",
                    WarrantyPeriod = 36,
                    IsDelete = false,
                    MinimumQuantity = 5,
                    MaximumQuantity = 100
                },
                new Product()
                {
                    Id = "PRN0009",
                    Name = "Canon LBP 2900 White",
                    Views = 2232,
                    OverView = "Chức năng: Print, Khổ giấy: A4/A5, In đảo mặt: Không, Cổng giao tiếp: USB, Dùng mực: Canon EP303",
                    Stars = 1,
                    RetailPrice = 2990000,
                    WholesalePrices = 2700000,
                    BrandId = "6c0d47b5-5b62-4c53-bd6e-680a3f08c1f0",
                    CategoryId = "610491a0-1824-4dd6-a8af-1f22f2a840a4",
                    WarrantyPeriod = 36,
                    IsDelete = false,
                    MinimumQuantity = 5,
                    MaximumQuantity = 100
                },
                new Product()
                {
                    Id = "PRN0010",
                    Name = "Logitech G102 Black",
                    Views = 2232,
                    OverView = "8000 DPI, LED RGB 16,8 triệu màu tùy chỉnh, Phù hợp với Gaming",
                    Stars = 1,
                    RetailPrice = 290000,
                    WholesalePrices = 270000,
                    BrandId = "fb5f1a2e-796a-4a38-9fb3-db9f4924c201",
                    CategoryId = "f09b42c7-79f6-406e-8b32-d1bd714f93cf",
                    WarrantyPeriod = 12,
                    IsDelete = false,
                    MinimumQuantity = 5,
                    MaximumQuantity = 100
                },
                new Product()
                {
                    Id = "PRN0011",
                    Name = "Logitech G102 White",
                    Views = 2232,
                    OverView = "8000 DPI, LED RGB 16,8 triệu màu tùy chỉnh, Phù hợp với Gaming",
                    Stars = 1,
                    RetailPrice = 290000,
                    WholesalePrices = 270000,
                    BrandId = "fb5f1a2e-796a-4a38-9fb3-db9f4924c201",
                    CategoryId = "f09b42c7-79f6-406e-8b32-d1bd714f93cf",
                    WarrantyPeriod = 12,
                    IsDelete = false,
                    MinimumQuantity = 5,
                    MaximumQuantity = 100
                },
                new Product()
                {
                    Id = "PRN0012",
                    Name = "Razer Hammerhead Pro V2",
                    Views = 2232,
                    Stars = 1,
                    RetailPrice = 1290000,
                    WholesalePrices = 1285000,
                    BrandId = "25a44ea6-ca33-494c-90d7-933e38ec3fb5",
                    CategoryId = "f09b42c7-79f6-406e-8b32-d1bd714f93cf",
                    WarrantyPeriod = 12,
                    IsDelete = false,
                    MinimumQuantity = 5,
                    MaximumQuantity = 100
                },
                new Product()
                {
                    Id = "PRN0013",
                    Name = "Razer Kraken Pro V2",
                    Views = 2232,
                    Stars = 1,
                    RetailPrice = 2290000,
                    WholesalePrices = 2285000,
                    BrandId = "25a44ea6-ca33-494c-90d7-933e38ec3fb5",
                    CategoryId = "f09b42c7-79f6-406e-8b32-d1bd714f93cf",
                    WarrantyPeriod = 12,
                    IsDelete = false,
                    MinimumQuantity = 5,
                    MaximumQuantity = 100
                },
                new Product()
                {
                    Id = "PRN0014",
                    Name = "Asus Nitro 5",
                    Views = 2100,
                    OverView = "I5-2.6GHz/8GB/256GB/New-Fullbox",
                    Stars = 1,
                    RetailPrice = 12990000,
                    WholesalePrices = 12900000,
                    BrandId = "82fab56a-dc26-4179-9624-d4eac7f43923",
                    CategoryId = "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2",
                    WarrantyPeriod = 36,
                    IsDelete = false,
                    MinimumQuantity = 5,
                    MaximumQuantity = 100
                },
                new Product()
                {
                    Id = "PRN0015",
                    Name = "Asus Predator",
                    Views = 2100,
                    OverView = "I9-3.6GHz/16GB/1TB/New-Fullbox",
                    Stars = 1,
                    RetailPrice = 52990000,
                    WholesalePrices = 52900000,
                    BrandId = "82fab56a-dc26-4179-9624-d4eac7f43923",
                    CategoryId = "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2",
                    WarrantyPeriod = 36,
                    IsDelete = false,
                    MinimumQuantity = 5,
                    MaximumQuantity = 100
                },
                new Product()
                {
                    Id = "PRN0016",
                    Name = "Asus ROG Strix",
                    Views = 2100,
                    OverView = "I7-3.6GHz/8GB/512GB/New-Fullbox",
                    Stars = 1,
                    RetailPrice = 22990000,
                    WholesalePrices = 22900000,
                    BrandId = "82fab56a-dc26-4179-9624-d4eac7f43923",
                    CategoryId = "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2",
                    WarrantyPeriod = 36,
                    IsDelete = false,
                    MinimumQuantity = 5,
                    MaximumQuantity = 100
                },
                new Product()
                {
                    Id = "PRN0017",
                    Name = "Asus Zenbook",
                    Views = 2100,
                    OverView = "I5-3.6GHz/8GB/128GB/New-Fullbox",
                    Stars = 1,
                    RetailPrice = 13990000,
                    WholesalePrices = 13900000,
                    BrandId = "82fab56a-dc26-4179-9624-d4eac7f43923",
                    CategoryId = "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2",
                    WarrantyPeriod = 36,
                    IsDelete = false,
                    MinimumQuantity = 5,
                    MaximumQuantity = 100
                },
                new Product()
                {
                    Id = "PRN0018",
                    Name = "Thinkpad X250",
                    Views = 2100,
                    OverView = "I5-2.6GHz/8GB/128GB/New-Fullbox",
                    Stars = 1,
                    RetailPrice = 9990000,
                    WholesalePrices = 9900000,
                    BrandId = "bc240879-0f96-4752-9632-82141e4f23b3",
                    CategoryId = "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2",
                    WarrantyPeriod = 36,
                    IsDelete = false,
                    MinimumQuantity = 5,
                    MaximumQuantity = 100
                },
                new Product()
                {
                    Id = "PRN0019",
                    Name = "Thinkpad X260",
                    Views = 2100,
                    OverView = "I5-3.1GHz/8GB/128GB/New-Fullbox",
                    Stars = 1,
                    RetailPrice = 10990000,
                    WholesalePrices = 10900000,
                    BrandId = "bc240879-0f96-4752-9632-82141e4f23b3",
                    CategoryId = "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2",
                    WarrantyPeriod = 36,
                    IsDelete = false,
                    MinimumQuantity = 5,
                    MaximumQuantity = 100
                });
        }
    }
}
