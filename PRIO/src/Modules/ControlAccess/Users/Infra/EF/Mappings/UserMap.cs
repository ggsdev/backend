using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable
                    ("Users");

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.Username)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.Password)
                .HasColumnType("VARCHAR")
                .HasMaxLength(90);

            builder.Property(x => x.Email)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150);

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.IsActive);

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.HasIndex(x => x.Username)
                .IsUnique();
        }
    }
}
