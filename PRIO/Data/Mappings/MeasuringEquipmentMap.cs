using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models;

namespace PRIO.Data.Mappings
{
    public class MeasuringEquipmentMap : IEntityTypeConfiguration<MeasuringEquipment>
    {
        public void Configure(EntityTypeBuilder<MeasuringEquipment> builder)
        {
            builder.ToTable("MeasuringEquipments");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Type)
                .HasColumnType("varchar")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.TagEquipment)
                .HasColumnType("varchar")
                .HasMaxLength(120)
                .IsRequired();
            builder.Property(x => x.TagMeasuringPoint)
                .HasColumnType("varchar")
                .HasMaxLength(120)
                .IsRequired();
            builder.Property(x => x.Fluid)
                .HasColumnType("varchar")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnType("text");

            builder.HasOne(x => x.Installation)
                .WithMany(d => d.MeasuringEquipments)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
                .WithMany(d => d.MeasuringEquipments)
                .OnDelete(DeleteBehavior.SetNull);
        }

    }
}
