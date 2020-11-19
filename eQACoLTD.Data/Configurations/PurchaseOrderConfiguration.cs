using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eQACoLTD.Data.Configurations
{
    public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.ToTable("PurchaseOrders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("varchar(12)");
            builder.Property(x => x.DateCreated).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            builder.Property(x => x.IsDelete).HasDefaultValue(false);
            builder.Property(x => x.DiscountValue).HasColumnType("decimal").HasDefaultValue(0);
            builder.Property(x => x.DiscountDescription).HasColumnType("nvarchar(500)");
            builder.Property(x => x.Description).HasColumnType("nvarchar(300)");
            builder.Property(x => x.DiscountType).HasColumnType("char(1)");
            builder.Property(x => x.DeliveryDate).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            builder.Property(x => x.TotalAmount).HasColumnType("decimal");

            builder.HasOne(s => s.Supplier)
                .WithMany(p => p.PurchaseOrders)
                .HasForeignKey(p => p.SupplierId);
            builder.HasOne(b => b.Branch)
                .WithMany(p => p.PurchaseOrders)
                .HasForeignKey(p => p.BrandId);
            builder.HasOne(t => t.TransactionStatus)
                .WithMany(p => p.PurchaseOrders)
                .HasForeignKey(p => p.TransactionStatusId);
            builder.HasOne(e => e.Employee)
                .WithMany(p => p.PurchaseOrders)
                .HasForeignKey(p => p.EmployeeId);
            builder.HasOne(p => p.PaymentStatus)
                .WithMany(p => p.PurchaseOrders)
                .HasForeignKey(p => p.PaymentStatusId);

            builder.HasData(
                new PurchaseOrder()
                {
                    Id = "PON0001",
                    SupplierId = "SUN0001",
                    PaymentStatusId = "4cc5fe42-6e47-4d47-a205-96039474bdac",
                    IsDelete = false,
                    DeliveryDate = DateTime.Now,
                    DateCreated = DateTime.Now,
                    BrandId = "ec4c314e-90b1-464c-aa52-2d34e555875e",
                    TransactionStatusId = "1fd31639-0fa6-4ac2-bbf2-f8dbd6e1f3c8",
                    EmployeeId = "EPN0001",
                    DiscountType = "$",
                    DiscountValue = 0,
                    TotalAmount=1380000000
                },
                new PurchaseOrder()
                {
                    Id = "PON0002",
                    SupplierId = "SUN0002",
                    PaymentStatusId = "03696c5b-71ad-4476-a0af-e52568d4b645",
                    IsDelete = false,
                    DeliveryDate = DateTime.Now,
                    DateCreated = DateTime.Now,
                    BrandId = "ec4c314e-90b1-464c-aa52-2d34e555875e",
                    TransactionStatusId = "4226c92d-694f-4948-afd4-04c636fd77a6",
                    EmployeeId = "EPN0001",
                    DiscountType = "$",
                    DiscountValue = 0,
                    TotalAmount=735700000
                },
                new PurchaseOrder()
                {
                    Id = "PON0003",
                    SupplierId = "SUN0003",
                    PaymentStatusId = "03696c5b-71ad-4476-a0af-e52568d4b645",
                    IsDelete = false,
                    DeliveryDate = DateTime.Now,
                    DateCreated = DateTime.Now,
                    BrandId = "ec4c314e-90b1-464c-aa52-2d34e555875e",
                    TransactionStatusId = "4226c92d-694f-4948-afd4-04c636fd77a6",
                    EmployeeId = "EPN0001",
                    DiscountType = "$",
                    DiscountValue = 0,
                    TotalAmount=158390000 
                });

        }
    }
}
