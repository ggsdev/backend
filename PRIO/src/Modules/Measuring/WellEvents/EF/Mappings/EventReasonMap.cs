using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.WellEvents.EF.Models;

namespace PRIO.src.Modules.Measuring.WellEvents.EF.Mappings
{
    public class EventReasonMap : IEntityTypeConfiguration<EventReason>
    {
        public void Configure(EntityTypeBuilder<EventReason> builder)
        {
            builder.ToTable
                    ("EventReasons");

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

            builder.Property(x => x.Downtime)
               .HasColumnType("CHAR")
               .HasMaxLength(15)
               .IsRequired();

            builder.Property(x => x.Reason)
               .HasColumnType("VARCHAR")
               .HasMaxLength(1000)
               .IsRequired();

            builder.HasOne(c => c.WellEvent)
                .WithMany(u => u.EventReasons)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
