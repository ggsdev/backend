using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using PRIO.Models;

namespace PRIO.Data.Mappings
{
    public class SystemHistoryMap : IEntityTypeConfiguration<SystemHistory>
    {
        public void Configure(EntityTypeBuilder<SystemHistory> builder)
        {
            builder.ToTable("SystemHistories");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Table)
                  .HasColumnType("varchar")
                  .HasMaxLength(100)
                  .IsRequired();

            builder.Property(x => x.TypeOperation)
                .HasColumnType("varchar")
                  .HasMaxLength(15)
                  .IsRequired();

            builder.Property(x => x.PreviousData)
             .HasColumnType("varchar(max)")
             .IsRequired()
             .HasConversion(
                 v => JsonConvert.SerializeObject(v),
                 v => JsonConvert.DeserializeObject<object>(v)
             );

            builder.Property(x => x.UpdatedData)
                .HasColumnType("varchar(max)")
                .IsRequired()
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<object>(v)
                );

            builder.Property(x => x.FieldsChanged)
                .HasColumnType("varchar(max)")
                .IsRequired()
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<object>(v)
                );

            //public Guid Id { get; set; }
            //public DateTime CreatedAt { get; set; }
            //public string Table { get; set; } = string.Empty;
            //public Guid CreatedBy { get; set; }
            //public Guid UpdatedBy { get; set; }
            //public Guid TableItemId { get; set; }
            //public object? PreviousData { get; set; }
            //public object? UpdatedData { get; set; }
            //public object? FieldsChanged { get; set; }
            //public string TypeOperation { get; set; } = Utils.TypeOperation.Create;
        }
    }
}
