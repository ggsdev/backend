using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Mappings
{
    public class ImportGasMap : IEntityTypeConfiguration<ImportGas>
    {
        public void Configure(EntityTypeBuilder<ImportGas> builder)
        {
            builder.ToTable("ImportGases");

            builder.Property(x => x.Name)
               .HasColumnType("VARCHAR")
               .HasMaxLength(260)
               .IsRequired();

            builder.HasOne(x => x.MeasuringPoint)
                .WithOne(d => d.ImportGas)
                .HasForeignKey<ImportGas>("MeasuringPointId");

            builder.HasOne(x => x.GasVolumeCalculation)
                .WithMany(d => d.ImportGases);
        }
    }
}
