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

            //builder.Property(x => x.Interval)
            //    .HasColumnType("float")
            //    .HasPrecision(15, 6);
            builder.Property(x => x.SystemRelated)
               .HasColumnType("VARCHAR")
               .HasMaxLength(60)
               .IsRequired();

            builder.Property(x => x.Comment)
              .HasColumnType("text");

            builder.HasOne(c => c.WellEvent)
                .WithMany(u => u.EventReasons)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
