using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models.Reservoirs;

namespace PRIO.Data.Mappings.ReservoirMapping
{
    public class ReservoirMap : IEntityTypeConfiguration<Reservoir>
    {
        public void Configure(EntityTypeBuilder<Reservoir> builder)
        {
            builder.ToTable("Reservoirs");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.CodReservoir)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.DeletedAt);

            builder.Property(x => x.IsActive);

            builder.HasOne(c => c.User).
                WithMany(u => u.Reservoirs)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.Zone).
                WithMany(u => u.Reservoirs)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
