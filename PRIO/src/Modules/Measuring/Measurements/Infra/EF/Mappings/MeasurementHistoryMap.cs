using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.Measurements.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Measurements.Infra.EF.Mappings
{
    public class MeasurementHistoryMap : IEntityTypeConfiguration<MeasurementHistory>
    {

        public void Configure(EntityTypeBuilder<MeasurementHistory> builder)
        {
            builder.ToTable("MeasurementsHistories");

            builder.Property(x => x.ImportedAt)
                .HasColumnType("date");

            builder.Property(x => x.FileType)
                .HasColumnType("varchar")
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(x => x.FileName)
                .HasColumnType("varchar")
                .HasMaxLength(800)
                .IsRequired();

            builder.Property(x => x.FileAcronym)
                .HasColumnType("varchar")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.TypeOperation)
                .HasColumnType("varchar")
                .HasMaxLength(20)
                .IsRequired();

            builder.HasOne(x => x.ImportedBy)
            .WithMany(m => m.MeasurementsHistories)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
        }

    }
}
