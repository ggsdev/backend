using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Mappings
{
    public class PurgeGasMap : IEntityTypeConfiguration<PurgeGas>
    {
        public void Configure(EntityTypeBuilder<PurgeGas> builder)
        {
            builder.ToTable("PurgeGases");

            builder.Property(x => x.StaticLocalMeasuringPoint)
               .HasColumnType("VARCHAR")
               .HasMaxLength(260)
               .IsRequired();

            builder.HasOne(x => x.MeasuringPoint)
                .WithOne(d => d.PurgeGas)
                .HasForeignKey<PurgeGas>("MeasuringPointId");

            builder.HasOne(x => x.GasVolumeCalculation)
                .WithMany(d => d.PurgeGases);
        }
    }
}
