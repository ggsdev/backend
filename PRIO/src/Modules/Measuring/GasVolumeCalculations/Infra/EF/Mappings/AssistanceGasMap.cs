using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Mappings
{
    public class AssistanceGasMap : IEntityTypeConfiguration<AssistanceGas>
    {
        public void Configure(EntityTypeBuilder<AssistanceGas> builder)
        {
            builder.ToTable("ConfigCalc.AssistanceGases");

            builder.Property(x => x.StaticLocalMeasuringPoint)
               .HasColumnType("VARCHAR")
               .HasMaxLength(260)
               .IsRequired();

            builder.HasOne(x => x.MeasuringPoint)
                .WithOne(d => d.AssistanceGas)
                .HasForeignKey<AssistanceGas>("MeasuringPointId");

            builder.HasOne(x => x.GasVolumeCalculation)
                .WithMany(d => d.AssistanceGases);
        }
    }
}
