using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class CustomerTypeConfiguration : IEntityTypeConfiguration<CustomerType>
    {
        public void Configure(EntityTypeBuilder<CustomerType> builder)
        {
            builder.ToTable("CustomerTypes");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(x => x.Description).HasColumnType("nvarchar(250)");

            builder.HasData(
                new CustomerType()
                {
                    Id = "c876bee4-019b-4b42-acbd-e728e9f545b6",
                    Name = "Bán buôn"
                },
                new CustomerType()
                {
                    Id = "a3bc8a51-9264-4590-af51-5fd20812695a",
                    Name = "Bán lẻ"
                }
                );
        }
    }
}
