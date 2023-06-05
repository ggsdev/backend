using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models.Installations;

namespace PRIO.Data.Mappings.InstallationMapping
{
    public class InstallationMap : IEntityTypeConfiguration<Installation>
    {
        public void Configure(EntityTypeBuilder<Installation> builder)
        {
            builder.ToTable("Installations");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.CodInstallation)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.DeletedAt);

            builder.Property(x => x.IsActive);

            builder.HasOne(x => x.User)
               .WithMany(u => u.Installations)
               .OnDelete(DeleteBehavior.SetNull)
               .IsRequired();

            builder.HasOne(x => x.Cluster)
               .WithMany(u => u.Installations)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();
        }
    }
}
