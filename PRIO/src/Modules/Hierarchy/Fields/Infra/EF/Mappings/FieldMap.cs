using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Mappings
{
    public class FieldMap : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder.ToTable("Fields");

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.CodField)
                .HasColumnType("VARCHAR")
                .HasMaxLength(10);

            builder.Property(x => x.State)
               .HasColumnType("VARCHAR")
               .HasMaxLength(120);

            builder.Property(x => x.Basin)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.Location)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.Description)
               .HasColumnType("TEXT");

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.DeletedAt);

            builder.Property(x => x.IsActive);

            builder.HasOne(x => x.Installation)
                .WithMany(c => c.Fields)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(u => u.Fields)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
