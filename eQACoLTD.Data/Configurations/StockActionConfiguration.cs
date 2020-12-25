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

            builder.HasData(
                new StockAction()
                {
                    Id = "ec40371a-cd21-44f3-85a2-618ceb92a16f",
                    Name = "Nhập hàng nhà cung cấp"
                },
                new StockAction()
                {
                    Id = "e27503bd-12c6-4d8e-a68e-6296892134e2",
                    Name = "Xuất kho giao hàng cho khách/shipper"
                },
                new StockAction()
                {
                    Id = "066d0137-c819-4335-ab4a-b5659193b80b",
                    Name = "Nhập hàng khách trả"
                },
                new StockAction()
                {
                    Id = "8170a4e6-42c4-48b6-a9a6-20ef490e75be",
                    Name = "Nhập hàng bảo hành"
                });
        }
    }
}
