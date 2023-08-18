using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.Comments.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Comments.Infra.EF.Mappings
{
    public class CommentMap : IEntityTypeConfiguration<CommentInProduction>
    {
        public void Configure(EntityTypeBuilder<CommentInProduction> builder)
        {
            builder.ToTable("CommentsInProduction");

            builder.Property(x => x.Text)
              .HasColumnType("TEXT");

            builder.HasOne(x => x.CommentedBy)
                .WithMany(x => x.Comments)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Production)
                .WithOne(x => x.Comment)
                .HasForeignKey<CommentInProduction>("ProductionId")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
