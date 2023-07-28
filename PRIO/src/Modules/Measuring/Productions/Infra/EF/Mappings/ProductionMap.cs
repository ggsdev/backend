using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Mappings
{
    public class ProductionMap : IEntityTypeConfiguration<Production>
    {
        public void Configure(EntityTypeBuilder<Production> builder)
        {
            builder.ToTable("Productions");

            builder.Property(x => x.TotalOil)
               .HasColumnType("DECIMAL")
               .HasPrecision(10, 5);

            builder.Property(x => x.TotalGas)
              .HasColumnType("DECIMAL")
              .HasPrecision(10, 5);

            builder.HasOne(x => x.CalculatedImportedBy)
                .WithMany(d => d.Productions)
                .IsRequired();
        }
    }
}
