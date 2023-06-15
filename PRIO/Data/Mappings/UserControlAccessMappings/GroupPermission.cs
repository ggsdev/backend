using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models.UserControlAccessModels;

namespace PRIO.Data.Mappings.UserControlAccessMappings
{
    public class GroupPermissionMap : IEntityTypeConfiguration<GroupPermission>
    {
        public void Configure(EntityTypeBuilder<GroupPermission> builder)
        {

            builder.ToTable
                    ("GroupPermissions");

            builder.Property(x => x.GroupName)
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(x => x.MenuName)
                .IsRequired().HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.MenuRoute)
                .IsRequired()
                .HasColumnType("VARCHAR").HasMaxLength(90);

            builder.Property(x => x.MenuIcon)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.MenuOrder)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

        }
    }
}
