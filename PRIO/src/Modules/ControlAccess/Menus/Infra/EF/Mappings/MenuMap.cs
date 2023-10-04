using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Menus.Infra.EF.Mappings
{
    public class MenuMap : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("AC.Menus");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.Route)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(120);

            builder.Property(x => x.Icon)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(120);

            builder.HasOne(x => x.Parent)
                   .WithMany();

            builder.HasMany(x => x.Children)
                   .WithOne(x => x.Parent);

            builder.Property(x => x.Description)
                    .HasColumnType("TEXT");

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.DeletedAt);

            builder.Property(x => x.IsActive);
        }
    }
}
