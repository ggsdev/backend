using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models.Reservoirs;

namespace PRIO.Data.Mappings.ReservoirMapping
{
    public class ReservoirHistoryMap : IEntityTypeConfiguration<ReservoirHistory>
    {
        public void Configure(EntityTypeBuilder<ReservoirHistory> builder)
        {
            builder.ToTable("ReservoirHistories");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.NameOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.CodReservoir)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.ZoneOldId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasMaxLength(120);

            builder.Property(x => x.TypeOperation)
                .HasColumnType("VARCHAR")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.CodReservoirOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

            builder.Property(x => x.DescriptionOld)
                .HasColumnType("TEXT");

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.IsActive);

            builder.Property(x => x.IsActiveOld);

            builder.HasOne(c => c.User).
                WithMany(u => u.ReservoirHistories)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(x => x.Reservoir).
                WithMany(u => u.ReservoirHistories)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(x => x.Zone).
                WithMany(u => u.ReservoirHistories)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }

    }
}
