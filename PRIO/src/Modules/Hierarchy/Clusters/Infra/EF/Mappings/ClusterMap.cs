﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Clusters.Infra.EF.Mappings
{
    public class ClusterMap : IEntityTypeConfiguration<Cluster>
    {
        public void Configure(EntityTypeBuilder<Cluster> builder)
        {
            builder.ToTable
                    ("Hierachy.Clusters");

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

            builder.Property(x => x.Name)
               .HasColumnType("VARCHAR")
               .HasMaxLength(60)
               .IsRequired();

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.DeletedAt);

            builder.Property(x => x.IsActive);

            builder.Property(x => x.InactivatedAt);

            builder.HasOne(c => c.User).
                WithMany(u => u.Clusters)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
