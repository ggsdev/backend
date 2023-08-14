using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Mappings
{
    public class LowPressureGasMap : IEntityTypeConfiguration<LowPressureGas>
    {
        public void Configure(EntityTypeBuilder<LowPressureGas> builder)
        {
            builder.ToTable("LowPressureGases");

            builder.Property(x => x.StaticLocalMeasuringPoint)
               .HasColumnType("VARCHAR")
               .HasMaxLength(260)
               .IsRequired();

            builder.HasOne(x => x.MeasuringPoint)
                .WithOne(d => d.LowPressureGas)
                .HasForeignKey<LowPressureGas>("MeasuringPointId");

            builder.HasOne(x => x.GasVolumeCalculation)
                .WithMany(d => d.LowPressureGases);
        }
    }
}
