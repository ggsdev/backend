using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models.Completions;

namespace PRIO.Data.Mappings.CompletionMappings
{
    public class CompletionHistoryMap : IEntityTypeConfiguration<CompletionHistory>
    {
        public void Configure(EntityTypeBuilder<CompletionHistory> builder)
        {

            builder.ToTable
                    ("CompletionHistories");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.NameOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(256);

            builder.Property(x => x.CodCompletion)
                .HasColumnType("VARCHAR")
                .HasMaxLength(256);

            builder.Property(x => x.CodCompletionOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(256);

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

            builder.Property(x => x.DescriptionOld)
                .HasColumnType("TEXT");

            builder.Property(x => x.IsActive);

            builder.Property(x => x.IsActiveOld);

            builder.Property(x => x.CreatedAt);

            builder.HasOne(c => c.User).
                WithMany(u => u.CompletionHistories)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(x => x.Reservoir).
                WithMany(r => r.CompletionHistories)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Completion).
               WithMany(r => r.CompletionHistories)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();

            builder.HasOne(x => x.Well)
                .WithMany(c => c.CompletionHistories)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
