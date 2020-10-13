using eQACoLTD.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eQACoLTD.Data.Configurations
{
    public class TransactionStatusConfiguration:IEntityTypeConfiguration<TransactionStatus>
    {
        public void Configure(EntityTypeBuilder<TransactionStatus> builder)
        {
            builder.ToTable("TransactionStatuses");
            builder.Property(x => x.Id).HasColumnType("char(36)");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(100)");

            builder.HasData(
                new TransactionStatus()
                {
                    Id = "cc0c7f54-de94-481c-b662-36584002fe41",
                    Name = "Đang kiểm kho"
                },
                new TransactionStatus()
                {
                    Id = "1fd31639-0fa6-4ac2-bbf2-f8dbd6e1f3c8",
                    Name = "Đang giao dịch"
                },
                new TransactionStatus()
                {
                    Id = "4226c92d-694f-4948-afd4-04c636fd77a6",
                    Name = "Hoàn thành"
                },
                new TransactionStatus()
                {
                    Id = "38ed81d4-7606-458e-add1-bf4c03d035cb",
                    Name = "Hủy"
                },
                new TransactionStatus()
                {
                    Id = "7c2da6fd-28ab-496b-aa1b-bb343ec49d3a",
                    Name = "Đang chờ"
                },
                new TransactionStatus()
                {
                    Id = "138193dc-86b4-4c9d-a961-b7c1b1f7bed1",
                    Name = "Đang giao hàng"
                });
        }
    }
}