using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models.Permissions;

namespace PRIO.Data.Mappings.PermissionMappings
{
    public class PermissionMap : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.MenuName)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.GroupName)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(120);

            builder.Property(x => x.MenuRoute)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(120);

            builder.Property(x => x.MenuIcon)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(120);

            builder.Property(x => x.CreatedAt);

            builder.HasOne(x => x.User)
               .WithMany(u => u.Permissions)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();

            builder.HasOne(x => x.GroupMenu)
               .WithMany(u => u.Permissions)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();
        }
    }
}
