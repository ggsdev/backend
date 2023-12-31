﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Mappings
{
    public class ZoneProductionMap : IEntityTypeConfiguration<ZoneProduction>
    {
        public void Configure(EntityTypeBuilder<ZoneProduction> builder)
        {
            builder.ToTable("Production.ZoneProductions");

            builder.Property(x => x.GasProductionInZone)
              .HasColumnType("DECIMAL")
               .HasPrecision(38, 16);

            builder.Property(x => x.WaterProductionInZone)
                .HasColumnType("DECIMAL")
               .HasPrecision(38, 16);

            builder.Property(x => x.OilProductionInZone)
                .HasColumnType("DECIMAL")
               .HasPrecision(38, 16);

            builder.Property(x => x.ZoneId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.ProductionId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

        }
    }
}
