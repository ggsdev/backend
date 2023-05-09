using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Prio_BackEnd.Models;

namespace Prio_BackEnd.Data.Mappings
{
    public class ClusterMap: IEntityTypeConfiguration<Cluster>
    {
        public void Configure(EntityTypeBuilder<Cluster> builder)
        {

            builder.ToTable
                    ("Clusters");

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
                WithMany(u => u.Clusters)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Unit).
                WithMany(u => u.Clusters)
                .HasForeignKey("UnitId")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
