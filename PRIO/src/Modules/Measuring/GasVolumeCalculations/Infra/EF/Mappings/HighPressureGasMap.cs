using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Mappings
{
    public class HighPressureGasMap : IEntityTypeConfiguration<HighPressureGas>
    {
        public void Configure(EntityTypeBuilder<HighPressureGas> builder)
        {
            builder.ToTable("HighPressureGases");

            builder.Property(x => x.Name)
               .HasColumnType("VARCHAR")
               .HasMaxLength(260)
               .IsRequired();

            builder.HasOne(x => x.MeasuringPoint)
                .WithOne(d => d.HighPressureGas)
                .HasForeignKey<HighPressureGas>("MeasuringPointId");

            builder.HasOne(x => x.GasVolumeCalculation)
                .WithMany(d => d.HighPressureGases);
        }
    }
}
