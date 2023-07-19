using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Mappings
{
    public class LPFlareMap : IEntityTypeConfiguration<LPFlare>
    {
        public void Configure(EntityTypeBuilder<LPFlare> builder)
        {
            builder.ToTable("LPFlares");

            builder.Property(x => x.Name)
               .HasColumnType("VARCHAR")
               .HasMaxLength(260)
               .IsRequired();

            builder.HasOne(x => x.MeasuringPoint)
                .WithOne(d => d.LPFlare)
                .HasForeignKey<LPFlare>("MeasuringPointId");

            builder.HasOne(x => x.GasVolumeCalculation)
                .WithMany(d => d.LPFlares);
        }
    }
}
