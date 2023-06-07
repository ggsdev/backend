using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models.Clusters;

namespace PRIO.Data.Mappings.ClusterMapping
{
    public class ClusterMap : IEntityTypeConfiguration<Cluster>
    {
        public void Configure(EntityTypeBuilder<Cluster> builder)
        {

            builder.ToTable
                    ("Clusters");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

            builder.Property(x => x.UepCode)
                .HasColumnType("VARCHAR")
                .HasMaxLength(60);

            builder.Property(x => x.CodCluster)
                .HasColumnType("VARCHAR")
                .HasMaxLength(60);

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.DeletedAt);

            builder.Property(x => x.IsActive);

            builder.HasOne(c => c.User).
                WithMany(u => u.Clusters)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
