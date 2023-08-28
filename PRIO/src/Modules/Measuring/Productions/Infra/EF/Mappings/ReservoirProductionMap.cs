using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Mappings
{
    public class ReservoirProductionMap : IEntityTypeConfiguration<ReservoirProduction>
    {
        public void Configure(EntityTypeBuilder<ReservoirProduction> builder)
        {
            builder.ToTable("ReservoirProductions");

            builder.Property(x => x.GasProductionInReservoir)
              .HasColumnType("DECIMAL")
              .HasPrecision(14, 5);

            builder.Property(x => x.WaterProductionInReservoir)
                .HasColumnType("DECIMAL")
                .HasPrecision(14, 5);

            builder.Property(x => x.OilProductionInReservoir)
                .HasColumnType("DECIMAL")
                .HasPrecision(14, 5);

            builder.Property(x => x.ReservoirId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.ProductionId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

        }
    }
}
