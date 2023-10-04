using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Mappings
{
    public class CompletionProductionMap : IEntityTypeConfiguration<CompletionProduction>
    {
        public void Configure(EntityTypeBuilder<CompletionProduction> builder)
        {
            builder.ToTable("Production.CompletionProductions");

            builder.Property(x => x.GasProductionInCompletion)
              .HasColumnType("DECIMAL")
               .HasPrecision(38, 16);

            builder.Property(x => x.WaterProductionInCompletion)
                .HasColumnType("DECIMAL")
               .HasPrecision(38, 16);

            builder.Property(x => x.OilProductionInCompletion)
                .HasColumnType("DECIMAL")
               .HasPrecision(38, 16);

            builder.Property(x => x.CompletionId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.ProductionId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

        }
    }
}
