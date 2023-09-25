using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Mappings
{
    public class InstallationMap : IEntityTypeConfiguration<Installation>
    {
        public void Configure(EntityTypeBuilder<Installation> builder)
        {
            builder.ToTable("Hierachy.Installations");

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(x => x.CodInstallationAnp)
                .HasColumnType("VARCHAR")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(x => x.UepName)
                .HasColumnType("VARCHAR")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(x => x.UepCod)
                .HasColumnType("VARCHAR")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

            builder.Property(x => x.GasSafetyBurnVolume)
                .HasColumnType("decimal")
                .HasPrecision(10, 4);

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.DeletedAt);

            builder.Property(x => x.IsActive);

            builder.Property(x => x.InactivatedAt);

            builder.HasOne(x => x.User)
               .WithMany(u => u.Installations)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();

            builder.HasOne(x => x.Cluster)
               .WithMany(u => u.Installations)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();
        }
    }
}
