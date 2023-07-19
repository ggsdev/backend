using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Mappings
{
    public class ExportGasMap : IEntityTypeConfiguration<ExportGas>
    {
        public void Configure(EntityTypeBuilder<ExportGas> builder)
        {
            builder.ToTable("ExportGases");

            builder.Property(x => x.Name)
               .HasColumnType("VARCHAR")
               .HasMaxLength(260)
               .IsRequired();

            builder.HasOne(x => x.MeasuringPoint)
                .WithOne(d => d.ExportGas)
                .HasForeignKey<ExportGas>("MeasuringPointId");

            builder.HasOne(x => x.GasVolumeCalculation)
                .WithMany(d => d.ExportGases);
        }
    }
}
