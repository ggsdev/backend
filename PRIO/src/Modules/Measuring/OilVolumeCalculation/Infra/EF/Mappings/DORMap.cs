using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.Measuring.OilVolumeCalculation.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculation.Infra.EF.Mappings
{ 
    public class DORMap : IEntityTypeConfiguration<DOR>
    {
        public void Configure(EntityTypeBuilder<DOR> builder)
        {
            builder.ToTable("DORs");

            builder.Property(x => x.Name)
                .HasColumnType("varchar")
                .HasMaxLength(60)
                .IsRequired();

            builder.HasOne(x => x.Equipment)
                .WithOne(d => d.DOR)
                .HasForeignKey<DOR>("EquipmentId");

            builder.HasOne(x => x.OilVolumeCalculation)
                .WithMany(d => d.DORs);
        }
    }
    
}
