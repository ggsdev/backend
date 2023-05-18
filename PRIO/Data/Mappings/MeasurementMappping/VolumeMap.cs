using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models;

namespace PRIO.Data.Mappings.MeasurementMappping
{

    public class VolumeMap : IEntityTypeConfiguration<Volume>
    {

        public void Configure(EntityTypeBuilder<Volume> builder)
        {

            builder.ToTable("Volumes_039");

            builder.Property(x => x.DHA_MEDICAO_039)
               .HasColumnType("date")
               .IsRequired();

            builder.Property(x => x.DHA_MED_DECLARADO_039)
              .HasColumnType("decimal")
              .HasPrecision(7, 6)
              .IsRequired();

            builder.Property(x => x.DHA_MED_REGISTRADO_039)
              .HasColumnType("decimal")
              .HasPrecision(7, 6)
              .IsRequired();

        }
    }

}
