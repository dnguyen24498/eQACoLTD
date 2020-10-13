using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class WarehouseConfiguration:IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.ToTable("Warehouses");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(x => x.EmployeeId).IsRequired(false);

            builder.HasOne(e => e.Employee)
                .WithMany(w => w.Warehouses)
                .HasForeignKey(w => w.EmployeeId);
            builder.HasOne(b => b.Branch)
                .WithMany(w => w.Warehouses)
                .HasForeignKey(w => w.BranchId);

            builder.HasData(
                new Warehouse()
                {
                    Id = "d6bbee65-fe3d-4765-b569-202d9f3aa4f5",
                    Name = "Kho chính",
                    BranchId = "ec4c314e-90b1-464c-aa52-2d34e555875e"
                },
                new Warehouse()
                {
                    Id = "ac386e1b-3647-4457-92b5-3e003e726290",
                    Name = "Kho chứa sản phẩm bảo hành",
                    BranchId = "ec4c314e-90b1-464c-aa52-2d34e555875e"
                },
                new Warehouse()
                {
                    Id = "3dea6b77-c95b-48ff-bf50-746ad1714bbd",
                    Name = "Kho chứa sản phẩm thanh lý",
                    BranchId = "ec4c314e-90b1-464c-aa52-2d34e555875e"
                });
        }
    }
}