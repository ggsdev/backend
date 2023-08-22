using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.WellAppropriations.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.WellAppropriations.Infra.EF.Mappings
{
    public class WellAppropriationMap : IEntityTypeConfiguration<WellAppropriation>
    {
        public void Configure(EntityTypeBuilder<WellAppropriation> builder)
        {
            builder.ToTable("WellAppropriations");

            builder.Property(x => x.ProductionGasAsPercentageOfField)
                .HasColumnType("DECIMAL")
                .HasPrecision(7, 5);

            builder.Property(x => x.ProductionGasAsPercentageOfInstallation)
                .HasColumnType("DECIMAL")
                .HasPrecision(7, 5);

            builder.Property(x => x.ProductionGasInWell)
               .HasColumnType("DECIMAL")
               .HasPrecision(10, 5);

            builder.Property(x => x.ProductionOilAsPercentageOfField)
                .HasColumnType("DECIMAL")
                .HasPrecision(7, 5);

            builder.Property(x => x.ProductionOilAsPercentageOfInstallation)
                .HasColumnType("DECIMAL")
                .HasPrecision(7, 5);

            builder.Property(x => x.ProductionOilInWell)
               .HasColumnType("DECIMAL")
               .HasPrecision(10, 5);

            builder.Property(x => x.ProductionWaterAsPercentageOfField)
                .HasColumnType("DECIMAL")
                .HasPrecision(7, 5);

            builder.Property(x => x.ProductionWaterAsPercentageOfInstallation)
                .HasColumnType("DECIMAL")
                .HasPrecision(7, 5);

            builder.Property(x => x.ProductionWaterInWell)
               .HasColumnType("DECIMAL")
               .HasPrecision(10, 5);

            builder.HasOne(x => x.Production)
                .WithMany(x => x.WellAppropriations)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(x => x.BtpData)
                .WithMany(x => x.WellAppropriations)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
