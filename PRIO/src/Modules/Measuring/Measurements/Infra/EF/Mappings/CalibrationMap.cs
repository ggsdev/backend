﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Equipments.Infra.EF.Mappings
{

    public class CalibrationMap : IEntityTypeConfiguration<Calibration>
    {

        public void Configure(EntityTypeBuilder<Calibration> builder)
        {

            builder.ToTable("Measurement.Calibrations_039");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();


            builder.Property(x => x.DHA_FALHA_CALIBRACAO_039).HasColumnType("date");

            builder.Property(x => x.DHA_NUM_FATOR_CALIBRACAO_ANTERIOR_039).HasColumnType("float").HasPrecision(5, 5);

            builder.Property(x => x.DHA_NUM_FATOR_CALIBRACAO_ATUAL_039).HasColumnType("float").HasPrecision(5, 5);

            builder.Property(x => x.DHA_CERTIFICADO_ANTERIOR_039).HasColumnType("varchar").HasMaxLength(50);

            builder.Property(x => x.DHA_CERTIFICADO_ATUAL_039).HasColumnType("varchar").HasMaxLength(30);

            builder.HasOne(x => x.Measurement)
            .WithMany(m => m.LISTA_CALIBRACAO)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        }

    }
}
