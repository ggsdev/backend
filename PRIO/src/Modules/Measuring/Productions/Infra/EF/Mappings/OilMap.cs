﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Mappings
{
    public class OilMap : IEntityTypeConfiguration<Oil>
    {
        public void Configure(EntityTypeBuilder<Oil> builder)
        {
            builder.ToTable("Oils");

            builder.Property(x => x.TotalOil)
               .HasColumnType("DECIMAL")
               .HasPrecision(10, 5);

            builder.Property(x => x.Bsw)
               .HasColumnType("DECIMAL")
               .HasPrecision(4, 2);
        }
    }
}
