using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class ShippingOrderConfiguration : IEntityTypeConfiguration<Shipping>
    {
        public void Configure(EntityTypeBuilder<Shipping> builder)
        {
            builder.ToTable("ShippingOrders");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CustomerName).HasColumnType("nvarchar(150)");
            builder.Property(x => x.PhoneNumber).HasColumnType("varchar(30)");
            builder.Property(x => x.Fee).HasDefaultValue(0);
            builder.Property(x => x.Address).HasColumnType("varchar(300)");
            builder.Property(x => x.DateCreated).HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Description).HasColumnType("nvarchar(500)");
            builder.Property(x => x.CustomerId).IsRequired(false);
            
            builder.HasOne(c => c.Customer)
                .WithMany(so => so.ShippingOrders)
                .HasForeignKey(so => so.CustomerId);
            builder.HasOne(t => t.Transporter)
                .WithMany(so => so.ShippingOrders)
                .HasForeignKey(so => so.TransporterId);
            builder.HasOne(ss => ss.ShippingStatus)
                .WithMany(so => so.ShippingOrders)
                .HasForeignKey(so => so.ShippingStatusId);
        }
    }
}
