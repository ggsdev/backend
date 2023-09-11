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

            builder.Property(x => x.EfficienceLossOil)
               .HasColumnType("DECIMAL")
               .HasPrecision(22, 5);

            builder.Property(x => x.ProductionLostOil)
               .HasColumnType("DECIMAL")
               .HasPrecision(22, 5);

            builder.Property(x => x.Downtime)
               .HasMaxLength(15);

            builder.Property(x => x.ProportionalDayOil)
               .HasColumnType("DECIMAL")
               .HasPrecision(22, 5);

            builder.Property(x => x.ProportionalDayWater)
               .HasColumnType("DECIMAL")
               .HasPrecision(22, 5);

            builder.Property(x => x.ProductionLostGas)
              .HasColumnType("DECIMAL")
              .HasPrecision(22, 5);

            builder.Property(x => x.ProductionLostWater)
             .HasColumnType("DECIMAL")
             .HasPrecision(22, 5);

            builder.Property(x => x.ProportionalDayGas)
            .HasColumnType("DECIMAL")
            .HasPrecision(22, 5);

            builder.Property(x => x.EfficienceLossGas)
            .HasColumnType("DECIMAL")
            .HasPrecision(22, 5);

            builder.Property(x => x.EfficienceLossWater)
            .HasColumnType("DECIMAL")
            .HasPrecision(22, 5);
        }
    }
}
