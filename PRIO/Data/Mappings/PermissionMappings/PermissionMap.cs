using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models.Permissions;

namespace PRIO.Data.Mappings.PermissionMappings
{
    public class PermissionMap : IEntityTypeConfiguration<UserPermissions>
    {
        public void Configure(EntityTypeBuilder<UserPermissions> builder)
        {
            builder.ToTable("UserPermissions");

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
