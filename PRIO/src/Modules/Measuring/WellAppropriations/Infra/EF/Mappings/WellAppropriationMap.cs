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

            builder.Property(x => x.ProductionAsPercentageOfField)
                .HasColumnType("DECIMAL")
                .HasPrecision(4, 2);

            builder.Property(x => x.ProductionAsPercentageOfInstallation)
                .HasColumnType("DECIMAL")
                .HasPrecision(4, 2);

            builder.Property(x => x.ProductionInWell)
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
