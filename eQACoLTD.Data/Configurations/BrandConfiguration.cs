using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(100)");

            builder.HasData(
                new Brand()
                {
                    Id = "85000231-9235-48a6-b852-c64bdcc3376b",
                    Name = "Apple"
                },
                new Brand()
                {
                    Id = "df574e84-8df9-4da9-9686-52446cbd9a69",
                    Name = "Dell"
                },
                new Brand()
                {
                    Id = "0f3d454d-a999-44de-af81-e918e117f5e5",
                    Name = "HP"
                },
                new Brand()
                {
                    Id = "bc240879-0f96-4752-9632-82141e4f23b3",
                    Name = "Lenovo"
                },
                new Brand()
                {
                    Id = "82fab56a-dc26-4179-9624-d4eac7f43923",
                    Name = "Asus"
                },
                new Brand()
                {
                    Id = "41f4bb29-5373-459a-8f6d-def64f15f747",
                    Name = "Acer"
                },
                new Brand()
                {
                    Id = "25a44ea6-ca33-494c-90d7-933e38ec3fb5",
                    Name = "Razer"
                },
                new Brand()
                {
                    Id = "6c0d47b5-5b62-4c53-bd6e-680a3f08c1f0",
                    Name = "Canon"
                },
                new Brand()
                {
                    Id = "ce382131-49b5-49ea-9732-9d33e49874de",
                    Name = "ThinkView"
                },
                new Brand()
                {
                    Id = "3d3db375-7436-4783-93d4-3f1777538b4e",
                    Name = "Samsung"
                },
                new Brand()
                {
                    Id = "fb5f1a2e-796a-4a38-9fb3-db9f4924c201",
                    Name = "Logitech"
                });
        }
    }
}
