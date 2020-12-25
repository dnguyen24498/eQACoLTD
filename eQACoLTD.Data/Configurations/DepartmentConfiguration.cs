using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class DepartmentConfiguration:IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(100)");

            builder.HasData(
                new Department()
                {
                    Id = "023536ea-f6c4-40a1-a610-2e95dd0e4f2a",
                    Name = "Bộ phận kho"
                },
                new Department()
                {
                    Id = "2c33ac5e-c033-4eeb-b185-7f79007bbec3",
                    Name = "Bộ phận kinh doanh"
                },
                new Department()
                {
                    Id = "15d805f8-32fb-43f3-8c95-31e87c05d3e7",
                    Name = "Bộ phận bán hàng"
                },
                new Department()
                {
                    Id = "896571ab-344a-4258-b144-3f5c39b286e1",
                    Name = "Bộ phận thu ngân"
                },
                new Department()
                {
                    Id = "fd730f29-f41c-4d7f-a4a7-66bb8b2ac6b0",
                    Name = "Bộ phận kế toán"
                },
                new Department()
                {
                    Id = "aedb9def-b1ef-484a-ace0-d95471cf9421",
                    Name = "Bộ phận kỹ thuật"
                },
                new Department()
                {
                    Id = "8567cb14-668c-45f9-9c9a-182ee3b99981",
                    Name = "Bộ phận quản lý cấp cao"
                }
            );
        }
    }
}