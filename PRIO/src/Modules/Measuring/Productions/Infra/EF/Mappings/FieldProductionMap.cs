using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Mappings
{
    public class FieldProductionMap : IEntityTypeConfiguration<FieldProduction>
    {
        public void Configure(EntityTypeBuilder<FieldProduction> builder)
        {
            builder.ToTable("FieldsProductions");

            builder.Property(x => x.GasProductionInField)
              .HasColumnType("DECIMAL")
              .HasPrecision(10, 5);

            builder.Property(x => x.OilProductionInField)
                .HasColumnType("DECIMAL")
                .HasPrecision(10, 5);

            builder.Property(x => x.WaterProductionInField)
                .HasColumnType("DECIMAL")
                .HasPrecision(10, 5);

        }
    }
}
