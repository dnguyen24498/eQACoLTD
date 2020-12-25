using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class ShippingStatusConfiguration : IEntityTypeConfiguration<ShippingStatus>
    {
        public void Configure(EntityTypeBuilder<ShippingStatus> builder)
        {
            builder.ToTable("ShippingStatus");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(250)");

            builder.HasData(
                new ShippingStatus()
                {
                    Id = "cf4ff408-bdbc-4692-9b70-0e141269cfdb",
                    Name = "Đã đóng gói/Đợi giao hàng"
                },
                new ShippingStatus()
                {
                    Id = "e1bf07a3-4576-463d-940d-ecf284b80534",
                    Name = "Đang giao hàng"
                },
                new ShippingStatus()
                {
                    Id = "220966a7-8489-46dc-b621-97e834925554",
                    Name = "Đã giao hàng/Nhận tiền từ nhà vận chuyển"
                });
        }
    }
}
