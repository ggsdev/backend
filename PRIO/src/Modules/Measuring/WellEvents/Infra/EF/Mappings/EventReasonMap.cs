﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.WellEvents.Infra.EF.Mappings
{
    public class EventReasonMap : IEntityTypeConfiguration<EventReason>
    {
        public void Configure(EntityTypeBuilder<EventReason> builder)
        {
            builder.ToTable
                    ("Event.EventReasons");

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

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

            builder.HasOne(c => c.CreatedBy).
               WithMany(u => u.CreatedEventReasons)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();

            builder.HasOne(c => c.UpdatedBy).
               WithMany(u => u.UpdatedEventReasons)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
