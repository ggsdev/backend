using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models.Zones;

namespace PRIO.Data.Mappings.ZoneMappings
{
    public class ZoneHistoryMap : IEntityTypeConfiguration<ZoneHistory>
    {
        public void Configure(EntityTypeBuilder<ZoneHistory> builder)
        {
            builder.ToTable("ZoneHistories");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.CodZone)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.CodZoneOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

            builder.Property(x => x.DescriptionOld)
                .HasColumnType("TEXT");

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.IsActive);

            builder.Property(x => x.IsActiveOld);

            builder.HasOne(c => c.User).
                WithMany(u => u.ZoneHistories)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired();

            builder.HasOne(x => x.Zone).
                WithMany(u => u.ZoneHistories)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(x => x.Field)
                .WithMany(u => u.ZoneHistories)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}