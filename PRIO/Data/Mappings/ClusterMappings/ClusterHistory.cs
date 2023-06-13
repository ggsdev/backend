using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models.Clusters;

namespace PRIO.Data.Mappings.ClusterMapping
{
    public class ClusterHistoryMap : IEntityTypeConfiguration<ClusterHistory>
    {
        public void Configure(EntityTypeBuilder<ClusterHistory> builder)
        {
            builder.ToTable
                    ("ClusterHistories");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.NameOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(256);

            builder.Property(x => x.TypeOperation)
                .HasColumnType("VARCHAR")
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.CodClusterOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(60);

            builder.Property(x => x.CodCluster)
                .HasColumnType("VARCHAR")
                .HasMaxLength(60);

            builder.Property(x => x.DescriptionOld)
                .HasColumnType("TEXT");

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

            builder.Property(x => x.IsActiveOld);

            builder.Property(x => x.IsActive);

            builder.Property(x => x.CreatedAt);

            builder.HasOne(c => c.User).
                WithMany(u => u.ClusterHistories)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(c => c.Cluster).
                WithMany(u => u.ClusterHistories)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
