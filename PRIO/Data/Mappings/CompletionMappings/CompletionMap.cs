using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models.Completions;

namespace PRIO.Data.Mappings.CompletionMapping
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
                .HasColumnType("VARCHAR")
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.CodCompletion)
                .HasColumnType("VARCHAR")
                .HasMaxLength(256);

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.DeletedAt);

            builder.Property(x => x.IsActive);

            builder.HasOne(c => c.User).
                WithMany(u => u.Completions)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(x => x.Reservoir).
                WithMany(r => r.Completions)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(x => x.Well)
                .WithMany(c => c.Completions)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
