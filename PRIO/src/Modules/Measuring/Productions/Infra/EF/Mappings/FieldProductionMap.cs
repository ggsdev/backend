using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Mappings
{
    public class FieldProductionMap : IEntityTypeConfiguration<FieldProduction>
    {
        public void Configure(EntityTypeBuilder<FieldProduction> builder)
        {
            builder.ToTable("Production.FieldsProductions");

            builder.Property(x => x.GasProductionInField)
              .HasColumnType("DECIMAL")
               .HasPrecision(38, 16);

            builder.Property(x => x.OilProductionInField)
                .HasColumnType("DECIMAL")
               .HasPrecision(38, 16);

            builder.Property(x => x.WaterProductionInField)
                .HasColumnType("DECIMAL")
               .HasPrecision(38, 16);

            builder.Property(x => x.FieldId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.ProductionId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

        }
    }
}
