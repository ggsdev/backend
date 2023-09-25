using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Equipments.Infra.EF.Mappings
{

    public class VolumeMap : IEntityTypeConfiguration<Volume>
    {

        public void Configure(EntityTypeBuilder<Volume> builder)
        {

            builder.ToTable("Measurement.Volumes_039");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.DHA_MEDICAO_039).HasColumnType("date");

            builder.Property(x => x.DHA_MED_DECLARADO_039).HasColumnType("float").HasPrecision(8, 6);

            builder.Property(x => x.DHA_MED_REGISTRADO_039).HasColumnType("float").HasPrecision(8, 6);

            builder.HasOne(x => x.Measurement)
            .WithMany(m => m.LISTA_VOLUME)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        }
    }

}
