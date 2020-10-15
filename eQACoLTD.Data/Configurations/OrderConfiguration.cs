using System;
using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class OrderConfiguration:IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.Property(x => x.Id).HasColumnType("varchar(12)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DateCreated).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Description).HasColumnType("nvarchar(300)");
            builder.Property(x => x.DiscountValue).HasColumnType("decimal");
            builder.Property(x => x.DiscountDescription).HasColumnType("nvarchar(300)");
            builder.Property(x => x.DiscountType).HasColumnType("char(1)");
            builder.Property(x => x.TotalAmount).HasColumnType("decimal");
            builder.Property(x => x.CustomerName).HasColumnType("nvarchar(150)");
            builder.Property(x => x.CustomerAddress).HasColumnType("nvarchar(300)");
            builder.Property(x => x.CustomerPhone).HasColumnType("nvarchar(30)");
            builder.HasOne(c => c.Customer)
                .WithMany(s => s.Orders)
                .HasForeignKey(s => s.CustomerId);
            builder.HasOne(e => e.Employee)
                .WithMany(s => s.Orders)
                .HasForeignKey(s => s.EmployeeId);
            builder.HasOne(t => t.TransactionStatus)
                .WithMany(s => s.Orders)
                .HasForeignKey(s => s.TransactionStatusId);
            builder.HasOne(p => p.PaymentStatus)
                .WithMany(s => s.Orders)
                .HasForeignKey(s => s.PaymentStatusId);
            builder.HasOne(b => b.Branch)
                .WithMany(s => s.Orders)
                .HasForeignKey(s => s.BranchId);

            builder.HasData(
                new Order()
                {
                    Id = "SRN0001",
                    CustomerId = "CUS0001",
                    TransactionStatusId = "4226c92d-694f-4948-afd4-04c636fd77a6",
                    PaymentStatusId = "03696c5b-71ad-4476-a0af-e52568d4b645",
                    EmployeeId = "EPN0002",
                    BranchId = "ec4c314e-90b1-464c-aa52-2d34e555875e",
                    TotalAmount= 45000000,
                    DiscountType="$",
                    DiscountValue=0
                });
        }
    }
}