using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class StockActionConfiguration : IEntityTypeConfiguration<StockAction>
    {
        public void Configure(EntityTypeBuilder<StockAction> builder)
        {
            builder.ToTable("StockActions");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(250)");
        }
    }
}
