using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models;

namespace PRIO.Data.Mappings
{
    public class CompletionMap : IEntityTypeConfiguration<Completion>
    {
        public void Configure(EntityTypeBuilder<Completion> builder)
        {

            builder.ToTable
                    ("Completions");

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
                WithMany(u => u.Completions)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Reservoir).
                WithMany(r => r.Completions)
                .HasForeignKey("ReservoirId")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
