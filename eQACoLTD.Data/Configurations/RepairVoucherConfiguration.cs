using System;
using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class RepairVoucherConfiguration:IEntityTypeConfiguration<RepairVoucher>
    {
        public void Configure(EntityTypeBuilder<RepairVoucher> builder)
        {
            builder.ToTable("RepairVouchers");
            builder.Property(x => x.Id).HasColumnType("varchar(12)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CustomerName).HasColumnType("nvarchar(200)");
            builder.Property(x => x.PhoneNumber).HasColumnType("varchar(30)");
            builder.Property(x => x.Description).HasColumnType("nvarchar(300)");
            builder.Property(x => x.DateCreated).HasColumnType("datetime").HasDefaultValue(DateTime.Now);
            builder.Property(x => x.AppointmentDate).HasColumnType("datetime");
            builder.Property(x => x.IsDelete).HasColumnType("bit").HasDefaultValue(0);

            builder.HasOne(c => c.Customer)
                .WithMany(r => r.RepairVouchers)
                .HasForeignKey(r => r.CustomerId);
            builder.HasOne(b => b.Branch)
                .WithMany(r => r.RepairVouchers)
                .HasForeignKey(r => r.BranchId);
            builder.HasOne(e => e.Employee)
                .WithMany(r => r.RepairVouchers)
                .HasForeignKey(r => r.EmployeeId);
            builder.HasOne(w => w.Warranty)
                .WithOne(r => r.RepairVoucher)
                .HasForeignKey<Warranty>(r => r.RepairVoucherId);
        }
    }
}