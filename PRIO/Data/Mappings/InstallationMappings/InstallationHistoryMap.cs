using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models.Installations;

namespace PRIO.Data.Mappings.InstallationMapping
{
    public class InstallationHistoryMap : IEntityTypeConfiguration<InstallationHistory>
    {
        public void Configure(EntityTypeBuilder<InstallationHistory> builder)
        {
            builder.ToTable("InstallationHistories");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.NameOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.CodInstallation)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.CodInstallationOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

            builder.Property(x => x.DescriptionOld)
                .HasColumnType("TEXT");

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.IsActive);


            builder.Property(x => x.ClusterName)
                .HasColumnType("VARCHAR")
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.ClusterOldId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasMaxLength(256);

            builder.Property(x => x.ClusterNameOld)
               .HasColumnType("VARCHAR")
                .HasMaxLength(256);

            builder.HasOne(x => x.User)
               .WithMany(u => u.InstallationHistories)
               .OnDelete(DeleteBehavior.SetNull)
               .IsRequired();

            builder.HasOne(x => x.Installation)
               .WithMany(u => u.InstallationHistories)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();

            builder.HasOne(x => x.Cluster)
               .WithMany(u => u.InstallationHistories)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();
        }
    }
}