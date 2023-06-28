using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Mappings
{
    public class SectionMap : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.ToTable("Sections");

            builder.Property(x => x.Name)
             .HasColumnType("varchar")
             .HasMaxLength(60)
             .IsRequired();

            builder.HasOne(x => x.Equipment)
               .WithOne(d => d.Section)
               .HasForeignKey<Section>("EquipmentId");

            builder.HasOne(x => x.OilVolumeCalculation)
               .WithMany(d => d.Sections);
        }
    }
}
