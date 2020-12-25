using System;
using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class ReturnConfiguration:IEntityTypeConfiguration<Return>
    {
        public void Configure(EntityTypeBuilder<Return> builder)
        {
            builder.ToTable("Returns");
            builder.Property(x => x.Id).HasColumnType("varchar(12)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DateCreated).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            builder.Property(x => x.IsImport).HasColumnType("bit").HasDefaultValue(0);
            builder.Property(x => x.Description).HasColumnType("nvarchar(300)");

            builder.HasOne(s => s.Order)
                .WithMany(r => r.Returns)
                .HasForeignKey(r => r.OrderId);
            builder.HasOne(b => b.Branch)
                .WithMany(r => r.Returns)
                .HasForeignKey(r => r.BranchId);
            builder.HasOne(p => p.PurchaseOrder)
                .WithMany(r => r.Returns)
                .HasForeignKey(r => r.PurchaseOrderId);
            builder.HasOne(e => e.Employee)
                .WithMany(r => r.Returns)
                .HasForeignKey(r => r.EmployeeId);
            
        }
    }
}