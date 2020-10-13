using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class PaymentStatusConfiguration : IEntityTypeConfiguration<PaymentStatus>
    {
        public void Configure(EntityTypeBuilder<PaymentStatus> builder)
        {
            builder.ToTable("PaymentStatuses");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(250)");

            builder.HasData(
                new PaymentStatus()
                {
                    Id = "4d2a23f7-4d7b-4fcb-b69b-fa078487f9aa",
                    Name = "Chưa thanh toán"
                },
                new PaymentStatus()
                {
                    Id = "4cc5fe42-6e47-4d47-a205-96039474bdac",
                    Name = "Thanh toán một phần"
                },
                new PaymentStatus()
                {
                    Id = "03696c5b-71ad-4476-a0af-e52568d4b645",
                    Name = "Đã thanh toán"
                });
        }
    }
}
