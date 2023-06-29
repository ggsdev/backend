using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Mappings
{
    public class TOGRecoveredOilMap : IEntityTypeConfiguration<TOGRecoveredOil>
    {
        public void Configure(EntityTypeBuilder<TOGRecoveredOil> builder)
        {
            builder.ToTable("TOGRecoveredOils");

            builder.Property(x => x.Name)
             .HasColumnType("varchar")
             .HasMaxLength(60)
             .IsRequired();

            builder.HasOne(x => x.MeasuringPoint)
                   .WithOne(d => d.TOGRecoveredOil)
                   .HasForeignKey<TOGRecoveredOil>("EquipmentId");

            builder.HasOne(x => x.OilVolumeCalculation)
               .WithMany(d => d.TOGRecoveredOils);
        }
    }
}

