using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Equipments.Infra.EF.Mappings
{
    public class MeasuringEquipmentMap : IEntityTypeConfiguration<MeasuringEquipment>
    {
        public void Configure(EntityTypeBuilder<MeasuringEquipment> builder)
        {
            builder.ToTable("Hierarchy.MeasuringEquipments");

            builder.Property(x => x.TagEquipment)
                .HasColumnType("varchar")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(x => x.TagMeasuringPoint)
                .HasColumnType("varchar")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(x => x.SerieNumber)
                .HasColumnType("varchar")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(x => x.Type)
                .HasColumnType("varchar")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(x => x.TypeEquipment)
                .HasColumnType("varchar")
                .HasMaxLength(60);

            builder.Property(x => x.Model)
                .HasColumnType("varchar")
                .HasMaxLength(60);

            builder.Property(x => x.HasSeal);

            builder.Property(x => x.MVS);

            builder.Property(x => x.CommunicationProtocol)
                .HasColumnType("varchar")
                .HasMaxLength(60);

            builder.Property(x => x.ChannelNumber)
                .HasColumnType("varchar")
                .HasMaxLength(10);

            builder.Property(x => x.TypePoint)
                .HasColumnType("varchar")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(x => x.InOperation);

            builder.Property(x => x.Fluid)
                .HasColumnType("varchar")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnType("text");

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.DeletedAt);

            builder.Property(x => x.IsActive);

            builder.HasOne(x => x.MeasuringPoint)
                .WithMany(d => d.MeasuringEquipments);


            builder.HasOne(x => x.User)
                .WithMany(d => d.MeasuringEquipments)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
