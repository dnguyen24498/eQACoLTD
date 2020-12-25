using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.ToTable("OrderStatuses");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(250)");

            builder.HasData(
                new OrderStatus()
                {
                    Id = "0e3c8901-759e-4df0-aed3-2f7e6b0425ed",
                    Name = "Chờ xác nhận"
                },
                new OrderStatus()
                {
                    Id = "56c9e511-f469-4b29-a39f-c4bc5fa20428",
                    Name = "Đã xác nhận/Kiểm kho"
                },
                new OrderStatus()
                {
                    Id = "39b20679-3c2a-4a4b-b73d-441443438308",
                    Name = "Đã tạo"
                },
                new OrderStatus()
                {
                    Id = "384d0614-49bd-46c0-8db1-33fc6d0b34f2",
                    Name = "Hủy"
                });
        }
    }
}
