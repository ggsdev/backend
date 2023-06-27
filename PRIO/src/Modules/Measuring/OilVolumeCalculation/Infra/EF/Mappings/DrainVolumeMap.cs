﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.OilVolumeCalculation.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculation.Infra.EF.Mappings
{
    public class DrainVolumeMap : IEntityTypeConfiguration<DrainVolume>
    {
        public void Configure(EntityTypeBuilder<DrainVolume> builder)
        {
            builder.ToTable("DrainVolumes");

            builder.Property(x => x.Name)
             .HasColumnType("varchar")
             .HasMaxLength(60)
             .IsRequired();

            builder.HasOne(x => x.Equipment)
               .WithOne(d => d.DrainVolume)
               .HasForeignKey<DrainVolume>("EquipmentId");

            builder.HasOne(x => x.OilVolumeCalculation)
               .WithMany(d => d.DrainVolumes);
        }
    }
}
