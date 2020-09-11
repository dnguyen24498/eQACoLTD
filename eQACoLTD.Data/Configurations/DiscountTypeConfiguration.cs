using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class DiscountTypeConfiguration : IEntityTypeConfiguration<DiscountType>
    {
        public void Configure(EntityTypeBuilder<DiscountType> builder)
        {
            builder.ToTable("DiscountTypes");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnType("nvarchar(150)");
        }
    }
}
