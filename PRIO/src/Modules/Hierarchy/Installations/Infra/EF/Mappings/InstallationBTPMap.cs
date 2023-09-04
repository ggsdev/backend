using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Mappings
{
    public class InstallationBTPMap : IEntityTypeConfiguration<InstallationBTP>
    {
        public void Configure(EntityTypeBuilder<InstallationBTP> builder)
        {
            builder.ToTable("IntallationsBTPs");

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.DeletedAt);

            builder.Property(x => x.IsActive);

            builder.HasOne(x => x.BTP)
               .WithMany(u => u.BTPs)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();

            builder.HasOne(x => x.Installation)
               .WithMany(u => u.BTPs)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();
        }
    }
}
