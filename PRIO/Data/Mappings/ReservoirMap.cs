using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models;

namespace PRIO.Data.Mappings
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
                .IsRequired()
                .HasColumnType("VARCHAR");

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.HasOne(c => c.User).
                WithMany(u => u.Reservoirs)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Installation).
                WithMany(u => u.Reservoirs)
                .HasForeignKey("InstallationId")
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
