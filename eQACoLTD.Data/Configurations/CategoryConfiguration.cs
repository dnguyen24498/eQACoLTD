using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(x => x.Description).HasColumnType("nvarchar(250)");

            builder.HasData(
                new Category()
                {
                    Id = "e8a447d0-ab0c-4dbc-b11b-5bcb60af73e2",
                    Name = "Máy tính xách tay"
                },
                new Category()
                {
                    Id = "c3eaeb51-38c5-4ac4-bbb1-9cbeb8881525",
                    Name = "Máy tính để bàn"
                },
                new Category()
                {
                    Id = "a749d9d1-7621-4596-89f8-a6395778809b",
                    Name = "Tai nghe"
                },
                new Category()
                {
                    Id = "5f1d0891-37db-4d20-92e9-0a9da180ee62",
                    Name = "Bàn phím"
                },
                new Category()
                {
                    Id = "f09b42c7-79f6-406e-8b32-d1bd714f93cf",
                    Name = "Chuột"
                },
                new Category()
                {
                    Id = "610491a0-1824-4dd6-a8af-1f22f2a840a4",
                    Name = "Máy in"
                });
        }
    }
}
