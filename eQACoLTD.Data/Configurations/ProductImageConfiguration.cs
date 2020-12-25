using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsThumbnail).HasDefaultValue(false);
            builder.Property(x => x.Path).HasColumnType("nvarchar(1000)");

            builder.HasOne(p => p.Product)
                .WithMany(pi => pi.ProductImages)
                .HasForeignKey(pi => pi.ProductId);

            builder.HasData(
                new ProductImage()
                {
                    Id = "6eb2d8f5-999e-4b3e-8280-0fb58c6ef35f",
                    ProductId = "PRN0001",
                    IsThumbnail = true,
                    Path = "6eb2d8f5-999e-4b3e-8280-0fb58c6ef35f.png"
                },
                new ProductImage()
                {
                    Id = "bbe3a085-0b4e-4549-97b5-0ab47536c0db",
                    ProductId = "PRN0002",
                    IsThumbnail = true,
                    Path = "bbe3a085-0b4e-4549-97b5-0ab47536c0db.png"
                },
                new ProductImage()
                {
                    Id = "b8a45f7e-7174-47cc-87da-17b18d084b99",
                    ProductId = "PRN0003",
                    IsThumbnail = true,
                    Path = "b8a45f7e-7174-47cc-87da-17b18d084b99.png"
                },
                new ProductImage()
                {
                    Id = "9d2281d0-5bd9-4323-b79b-d65144af5338",
                    ProductId = "PRN0004",
                    IsThumbnail = true,
                    Path = "9d2281d0-5bd9-4323-b79b-d65144af5338.png"
                },
                new ProductImage()
                {
                    Id = "d791b7d6-661a-43fd-a0e8-32589639a346",
                    ProductId = "PRN0005",
                    IsThumbnail = true,
                    Path = "d791b7d6-661a-43fd-a0e8-32589639a346.png"
                },
                new ProductImage()
                {
                    Id = "bc091a22-5d49-4f0a-9d8b-1823e7df0b26",
                    ProductId = "PRN0006",
                    IsThumbnail = true,
                    Path = "bc091a22-5d49-4f0a-9d8b-1823e7df0b26.png"
                },
                new ProductImage()
                {
                    Id = "0b9c9f29-f557-49e6-8753-7018576f1f11",
                    ProductId = "PRN0007",
                    IsThumbnail = true,
                    Path = "0b9c9f29-f557-49e6-8753-7018576f1f11.png"
                },
                new ProductImage()
                {
                    Id = "126eb3fa-cd6c-4601-925b-c0e5754fe293",
                    ProductId = "PRN0008",
                    IsThumbnail = true,
                    Path = "126eb3fa-cd6c-4601-925b-c0e5754fe293.png"
                },
                new ProductImage()
                {
                    Id = "9e45320a-3b5f-45c1-988b-9b54a917ce41",
                    ProductId = "PRN0009",
                    IsThumbnail = true,
                    Path = "9e45320a-3b5f-45c1-988b-9b54a917ce41.png"
                },
                new ProductImage()
                {
                    Id = "648807b4-a97e-4e0c-bbef-c4c6198828b4",
                    ProductId = "PRN0010",
                    IsThumbnail = true,
                    Path = "648807b4-a97e-4e0c-bbef-c4c6198828b4.png"
                },
                new ProductImage()
                {
                    Id = "9f8e48be-e769-4222-9fad-0ca1c60d7e57",
                    ProductId = "PRN0011",
                    IsThumbnail = true,
                    Path = "9f8e48be-e769-4222-9fad-0ca1c60d7e57.png"
                },
                new ProductImage()
                {
                    Id = "9438d141-3011-4438-8ed3-85d7d375f255",
                    ProductId = "PRN0012",
                    IsThumbnail = true,
                    Path = "9438d141-3011-4438-8ed3-85d7d375f255.png"
                },
                new ProductImage()
                {
                    Id = "8b361a2c-5a87-44ed-a327-934f7147c641",
                    ProductId = "PRN0013",
                    IsThumbnail = true,
                    Path = "8b361a2c-5a87-44ed-a327-934f7147c641.png"
                },
                new ProductImage()
                {
                    Id = "53d1c2b4-6724-4ad4-b056-b6b26a830a8b",
                    ProductId = "PRN0014",
                    IsThumbnail = true,
                    Path = "53d1c2b4-6724-4ad4-b056-b6b26a830a8b.png"
                },
                new ProductImage()
                {
                    Id = "1699bbbb-5490-43a0-9219-8ec31b65e958",
                    ProductId = "PRN0015",
                    IsThumbnail = true,
                    Path = "1699bbbb-5490-43a0-9219-8ec31b65e958.png"
                },
                new ProductImage()
                {
                    Id = "4de34fba-6e86-42bf-ae6b-8a1d1aad8fc8",
                    ProductId = "PRN0016",
                    IsThumbnail = true,
                    Path = "4de34fba-6e86-42bf-ae6b-8a1d1aad8fc8.png"
                },
                new ProductImage()
                {
                    Id = "86ef26e0-d2cd-48a7-8dd5-63b769731d55",
                    ProductId = "PRN0017",
                    IsThumbnail = true,
                    Path = "86ef26e0-d2cd-48a7-8dd5-63b769731d55.png"
                },
                new ProductImage()
                {
                    Id = "dbe638f0-4ded-4bc4-b373-aac67c0698cb",
                    ProductId = "PRN0018",
                    IsThumbnail = true,
                    Path = "dbe638f0-4ded-4bc4-b373-aac67c0698cb.png"
                },
                new ProductImage()
                {
                    Id = "4eb16e67-92f7-41cb-ab49-547e3ab4fb70",
                    ProductId = "PRN0019",
                    IsThumbnail = true,
                    Path = "4eb16e67-92f7-41cb-ab49-547e3ab4fb70.png"
                });
            
        }
    }
}
