using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Mappings
{
    public class GasLinearMap : IEntityTypeConfiguration<GasLinear>
    {
        public void Configure(EntityTypeBuilder<GasLinear> builder)
        {
            builder.ToTable("GasesLinears");

            builder.Property(x => x.TotalGas)
              .HasColumnType("DECIMAL")
              .HasPrecision(14, 5);

            builder.Property(x => x.ExportedGas)
                .HasColumnType("DECIMAL")
                .HasPrecision(14, 5);

            builder.Property(x => x.ImportedGas)
                .HasColumnType("DECIMAL")
                .HasPrecision(14, 5);

            builder.Property(x => x.BurntGas)
               .HasColumnType("DECIMAL")
               .HasPrecision(14, 5);

            builder.Property(x => x.FuelGas)
               .HasColumnType("DECIMAL")
               .HasPrecision(14, 5);
        }
    }
}
