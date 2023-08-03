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

            builder.HasOne(x => x.Oil)
                .WithOne(d => d.Production)
                .HasForeignKey<Production>("OilId")
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.GasLinear)
                .WithOne(d => d.Production)
                .HasForeignKey<Production>("GasLinearId")
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.GasDiferencial)
                .WithOne(d => d.Production)
                .HasForeignKey<Production>("GasDiferencialId")
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Gas)
                .WithOne(d => d.Production)
                .HasForeignKey<Production>("GasId")
               .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.TotalProduction)
                .HasColumnType("decimal")
                .HasPrecision(10, 5);

            //builder.HasOne(x => x.Water)
            //  .WithOne(d => d.Production)
            //  .HasForeignKey<Production>("WaterId")
            // .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.CalculatedImportedBy)
               .WithMany(d => d.Productions)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Installation)
               .WithMany(d => d.Productions)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();
        }
    }
}
