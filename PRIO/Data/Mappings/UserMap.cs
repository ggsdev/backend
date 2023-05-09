using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Prio_BackEnd.Models;

namespace Prio_BackEnd.Data.Mappings
{
    public class UserMap: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable
                    ("Users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.Username)
                .IsRequired().HasColumnType("VARCHAR")
                .HasMaxLength(120); ;

            builder.Property(x => x.Password)
                .IsRequired()
                .HasColumnType("VARCHAR").HasMaxLength(90);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(150);

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.HasIndex(x => x.Email)
                .IsUnique();

        }
    }
}
