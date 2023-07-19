using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Mappings
{
    public class DrainVolumeMap : IEntityTypeConfiguration<DrainVolume>
    {
        public void Configure(EntityTypeBuilder<DrainVolume> builder)
        {
            builder.ToTable("DrainVolumes");

            builder.Property(x => x.Name)
               .HasColumnType("VARCHAR")
               .HasMaxLength(260)
               .IsRequired();

            builder.HasOne(x => x.MeasuringPoint)
               .WithOne(d => d.DrainVolume)
               .HasForeignKey<DrainVolume>("MeasuringPointId");

            builder.HasOne(x => x.OilVolumeCalculation)
               .WithMany(d => d.DrainVolumes);
        }
    }
}
