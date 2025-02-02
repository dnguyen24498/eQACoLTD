﻿using System;
using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class GoodsReceivedNoteConfiguration:IEntityTypeConfiguration<GoodsReceivedNote>
    {
        public void Configure(EntityTypeBuilder<GoodsReceivedNote> builder)
        {
            builder.ToTable("GoodsReceivedNotes");
            builder.Property(x => x.Id).HasColumnType("varchar(12)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ImportDate).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Description).HasColumnType("nvarchar(300)");
            builder.Property(x => x.PlacedLocation).HasColumnType("nvarchar(300)");

            builder.HasOne(p => p.PurchaseOrder)
                .WithMany(g => g.GoodsReceivedNote)
                .HasForeignKey(g => g.PurchaseOrderId);

            builder.HasOne(e => e.Employee)
                .WithMany(g => g.GoodsReceivedNotes)
                .HasForeignKey(g => g.EmployeeId);
            builder.HasOne(s => s.StockAction)
                .WithMany(g => g.GoodsReceivedNotes)
                .HasForeignKey(g => g.StockActionId);
            builder.HasOne(w => w.Warehouse)
                .WithMany(g => g.GoodsReceivedNotes)
                .HasForeignKey(g => g.WarehouseId);
            builder.HasOne(r => r.RepairVoucher)
                .WithMany(g => g.GoodsReceivedNote)
                .HasForeignKey(g => g.RepairVoucherId);
            builder.HasOne(r => r.Return)
                .WithMany(g => g.GoodsReceivedNote)
                .HasForeignKey(g => g.ReturnId);

            builder.HasData(
                new GoodsReceivedNote()
                {
                    Id = "GRN0001",
                    PurchaseOrderId = "PON0001",
                    ImportDate = DateTime.Now,
                    EmployeeId = "EPN0001",
                    StockActionId = "ec40371a-cd21-44f3-85a2-618ceb92a16f",
                    WarehouseId = "d6bbee65-fe3d-4765-b569-202d9f3aa4f5",
                },
                new GoodsReceivedNote()
                {
                    Id = "GRN0002",
                    PurchaseOrderId = "PON0002",
                    ImportDate = DateTime.Now,
                    EmployeeId = "EPN0001",
                    StockActionId = "ec40371a-cd21-44f3-85a2-618ceb92a16f",
                    WarehouseId = "d6bbee65-fe3d-4765-b569-202d9f3aa4f5",
                },
                new GoodsReceivedNote()
                {
                    Id = "GRN0003",
                    PurchaseOrderId = "PON0003",
                    ImportDate = DateTime.Now,
                    EmployeeId = "EPN0001",
                    StockActionId = "ec40371a-cd21-44f3-85a2-618ceb92a16f",
                    WarehouseId = "d6bbee65-fe3d-4765-b569-202d9f3aa4f5",
                });
        }
    }
}