using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Mappings
{
    public class WellLossesMap : IEntityTypeConfiguration<WellLosses>
    {
        public void Configure(EntityTypeBuilder<WellLosses> builder)
        {
            builder.ToTable("WellLosses");

            builder.Property(x => x.EfficienceLoss)
               .HasColumnType("DECIMAL")
               .HasPrecision(38, 16);

            builder.Property(x => x.ProductionLostOil)
               .HasColumnType("DECIMAL")
               .HasPrecision(38, 16);

            builder.Property(x => x.ProportionalDay)
               .HasColumnType("DECIMAL")
               .HasPrecision(38, 16);

            builder.Property(x => x.ProductionLostGas)
              .HasColumnType("DECIMAL")
               .HasPrecision(38, 16);

            builder.Property(x => x.ProductionLostWater)
             .HasColumnType("DECIMAL")
               .HasPrecision(38, 16);

            builder.Property(x => x.Downtime)
             .HasColumnType("DECIMAL")
               .HasPrecision(38, 16);

            builder.Property(x => x.ProductionLostWater)
             .HasColumnType("DECIMAL")
               .HasPrecision(38, 16);

        }
    }
}
