using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models;

namespace PRIO.Data.Mappings.MeasurementMappping
{

    public class CalibrationMap : IEntityTypeConfiguration<Calibration>
    {

        public void Configure(EntityTypeBuilder<Calibration> builder)
        {

            builder.ToTable("Calibrations_039");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();


            builder.Property(x => x.DHA_FALHA_CALIBRACAO_039)
             .HasColumnType("date")
             .IsRequired();

            builder.Property(x => x.DHA_NUM_FATOR_CALIBRACAO_ANTERIOR_039)
             .HasColumnType("decimal")
             .HasPrecision(5, 5)
             .IsRequired();

            builder.Property(x => x.DHA_NUM_FATOR_CALIBRACAO_ATUAL_039)
             .HasColumnType("decimal")
             .HasPrecision(5, 5)
             .IsRequired();

            builder.Property(x => x.DHA_CERTIFICADO_ANTERIOR_039)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.DHA_CERTIFICADO_ATUAL_039)
                .HasColumnType("varchar")
                .HasMaxLength(30)
                .IsRequired();
        }

    }
}
