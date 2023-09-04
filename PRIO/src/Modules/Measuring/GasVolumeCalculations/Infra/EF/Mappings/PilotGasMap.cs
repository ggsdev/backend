using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Mappings
{
    public class PilotGasMap : IEntityTypeConfiguration<PilotGas>
    {
        public void Configure(EntityTypeBuilder<PilotGas> builder)
        {
            builder.ToTable("PilotGases");

            builder.Property(x => x.StaticLocalMeasuringPoint)
               .HasColumnType("VARCHAR")
               .HasMaxLength(260)
               .IsRequired();

            builder.HasOne(x => x.MeasuringPoint)
                .WithOne(d => d.PilotGas)
                .HasForeignKey<PilotGas>("MeasuringPointId");

            builder.HasOne(x => x.GasVolumeCalculation)
                .WithMany(d => d.PilotGases);
        }
    }
}
