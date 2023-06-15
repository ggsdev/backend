﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models.MeasurementModels;

namespace PRIO.Data.Mappings.MeasurementMappping
{
    public class BswMap : IEntityTypeConfiguration<Bsw>
    {

        public void Configure(EntityTypeBuilder<Bsw> builder)
        {

            builder.ToTable("BSWS_039");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();


            builder.Property(x => x.DHA_FALHA_BSW_039)
               .HasColumnType("date")
               ;
            builder.Property(x => x.DHA_PCT_BSW_039)
                   .HasColumnType("float")
                   .HasPrecision(3, 2)
                   ;
            builder.Property(x => x.DHA_PCT_MAXIMO_BSW_039).HasColumnType("float").HasPrecision(3, 2);

            builder.HasOne(x => x.Measurement)
                .WithMany(m => m.LISTA_BSW)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
