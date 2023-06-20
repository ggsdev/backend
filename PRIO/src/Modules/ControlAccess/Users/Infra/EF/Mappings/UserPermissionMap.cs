using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Mappings
{
    public class UserPermissionMap : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.ToTable("UserPermissions");


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
               .WithMany(u => u.UserPermissions)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();

            builder.HasOne(x => x.GroupMenu)
               .WithMany(u => u.Permissions)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();
        }
    }
}
