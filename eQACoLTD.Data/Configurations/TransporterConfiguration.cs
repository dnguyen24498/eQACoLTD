using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class TransporterConfiguration : IEntityTypeConfiguration<Transporter>
    {
        public void Configure(EntityTypeBuilder<Transporter> builder)
        {
            builder.ToTable("Transporters");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(150)");
            builder.Property(x => x.PhoneNumber).IsRequired().HasColumnType("varchar(30)");
        }
    }
}
