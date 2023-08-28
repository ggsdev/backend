﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.WellProductions.Infra.EF.Mappings
{
    public class WellProductionMap : IEntityTypeConfiguration<WellProduction>
    {
        public void Configure(EntityTypeBuilder<WellProduction> builder)
        {
            builder.ToTable("WellProductions");

            builder.Property(x => x.ProductionGasAsPercentageOfField)
                .HasColumnType("DECIMAL")
                .HasPrecision(7, 5);

            builder.Property(x => x.ProductionGasAsPercentageOfInstallation)
                .HasColumnType("DECIMAL")
                .HasPrecision(7, 5);

            builder.Property(x => x.ProductionGasInWell)
               .HasColumnType("DECIMAL")
               .HasPrecision(14, 5);

            builder.Property(x => x.ProductionOilAsPercentageOfField)
                .HasColumnType("DECIMAL")
                .HasPrecision(7, 5);

            builder.Property(x => x.ProductionOilAsPercentageOfInstallation)
                .HasColumnType("DECIMAL")
                .HasPrecision(7, 5);

            builder.Property(x => x.ProductionOilInWell)
               .HasColumnType("DECIMAL")
               .HasPrecision(14, 5);

            builder.Property(x => x.ProductionWaterAsPercentageOfField)
                .HasColumnType("DECIMAL")
                .HasPrecision(7, 5);

            builder.Property(x => x.ProductionWaterAsPercentageOfInstallation)
                .HasColumnType("DECIMAL")
                .HasPrecision(7, 5);

            builder.Property(x => x.ProductionWaterInWell)
               .HasColumnType("DECIMAL")
               .HasPrecision(14, 5);

            builder.HasOne(x => x.Production)
                .WithMany(x => x.WellProductions)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(x => x.BtpData)
                .WithMany(x => x.WellAppropriations)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
