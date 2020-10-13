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
            builder.Property(x => x.PhoneNumber).HasColumnType("varchar(30)");
            builder.Property(x => x.Address).HasColumnType("nvarchar(500)");

            builder.HasData(
                new Transporter()
                {
                    Id = "9d82a2e6-b9d9-4a89-977b-e94dd965e648",
                    Name = "Giao hàng tiết kiệm",
                    PhoneNumber = "0947685921",
                    Address = "Số 347, Lạch Tray, Ngô Quyền, Hải Phòng"
                },
                new Transporter()
                {
                    Id = "0ac9fb8f-c352-498d-9129-229fd5a080fa",
                    Name = "Giao hàng nhanh",
                    PhoneNumber = "0947325921",
                    Address = "Số 647, Lạch Tray, Ngô Quyền, Hải Phòng"
                },
                new Transporter()
                {
                    Id = "b6f71e5d-ea3b-4bbb-bbd4-b0f35acc052e",
                    Name = "Viettel Post",
                    PhoneNumber = "09476845921",
                    Address = "Số 147, Lạch Tray, Ngô Quyền, Hải Phòng"
                });
        }
    }
}
