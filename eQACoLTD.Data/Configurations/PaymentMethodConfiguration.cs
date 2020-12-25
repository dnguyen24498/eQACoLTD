using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable("PaymentMethods");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(250)");

            builder.HasData(
                new PaymentMethod()
                {
                    Id = "7cd60e3f-c215-42b3-a98e-c4ac4fe71b63",
                    Name = "Tiền mặt"
                },
                new PaymentMethod()
                {
                    Id = "a196f0c3-c36a-4cb1-892c-3c72e1dd8b02",
                    Name = "Quẹt thẻ"
                },
                new PaymentMethod()
                {
                    Id = "f178a9b0-13fa-4221-90cc-7cede6995026",
                    Name = "Điểm tích lũy"
                },
                new PaymentMethod()
                {
                    Id = "93f58e8a-7b32-4f80-a128-1c3dc5b50eda",
                    Name = "Chuyển khoản"
                });
        }
    }
}
