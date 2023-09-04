using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Mappings
{
    public class WellProductionMap : IEntityTypeConfiguration<Models.WellProductions>
    {
        public void Configure(EntityTypeBuilder<Models.WellProductions> builder)
        {
            builder.ToTable("WellProductions");

            builder.Property(x => x.ProductionGasAsPercentageOfField)
                .HasColumnType("DECIMAL")
                .HasPrecision(7, 5);

            builder.Property(x => x.ProductionGasAsPercentageOfInstallation)
                .HasColumnType("DECIMAL")
                .HasPrecision(7, 5);

            builder.Property(x => x.ProductionGasInWellM3)
               .HasColumnType("DECIMAL")
               .HasPrecision(14, 5);

            builder.Property(x => x.ProductionGasInWellSCF)
               .HasColumnType("DECIMAL")
               .HasPrecision(14, 5);

            builder.Property(x => x.ProductionOilAsPercentageOfField)
                .HasColumnType("DECIMAL")
                .HasPrecision(7, 5);

            builder.Property(x => x.ProductionOilAsPercentageOfInstallation)
                .HasColumnType("DECIMAL")
                .HasPrecision(7, 5);

            builder.Property(x => x.ProductionOilInWellM3)
               .HasColumnType("DECIMAL")
               .HasPrecision(14, 5);

            builder.Property(x => x.ProductionOilInWellBBL)
               .HasColumnType("DECIMAL")
               .HasPrecision(14, 5);

            builder.Property(x => x.ProductionWaterAsPercentageOfField)
                .HasColumnType("DECIMAL")
                .HasPrecision(7, 5);

            builder.Property(x => x.ProductionWaterAsPercentageOfInstallation)
                .HasColumnType("DECIMAL")
                .HasPrecision(7, 5);

            builder.Property(x => x.ProductionWaterInWellM3)
               .HasColumnType("DECIMAL")
               .HasPrecision(14, 5);

            builder.Property(x => x.ProductionWaterInWellBBL)
               .HasColumnType("DECIMAL")
               .HasPrecision(14, 5);

            builder.Property(x => x.EfficienceLoss)
               .HasColumnType("DECIMAL")
               .HasPrecision(14, 5);

            builder.Property(x => x.ProductionLost)
               .HasColumnType("DECIMAL")
               .HasPrecision(14, 5);

            builder.Property(x => x.Downtime)
               .HasMaxLength(15);

            builder.Property(x => x.ProportionalDay)
               .HasColumnType("DECIMAL")
               .HasPrecision(14, 5);

            builder.HasOne(x => x.Production)
                .WithMany(x => x.WellProductions)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(x => x.WellTest)
                .WithMany(x => x.WellAllocations)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
