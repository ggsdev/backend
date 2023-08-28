using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.WellEvents.EF.Models;

namespace PRIO.src.Modules.Measuring.WellEvents.EF.Mappings
{
    public class WellEventMap : IEntityTypeConfiguration<WellEvent>
    {
        public void Configure(EntityTypeBuilder<WellEvent> builder)
        {
            builder.ToTable
                    ("WellEvents");

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

            builder.Property(x => x.Downtime)
               .HasColumnType("CHAR")
               .HasMaxLength(8)
               .IsRequired();

            builder.HasOne(c => c.Well).
                WithMany(u => u.WellEvents)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
