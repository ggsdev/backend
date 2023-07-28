using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.Equipments.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Equipments.Infra.EF.Mappings
{
    public class FileTypeMap : IEntityTypeConfiguration<FileType>
    {
        public void Configure(EntityTypeBuilder<FileType> builder)
        {
            builder.ToTable("FileTypes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
               .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.Acronym)
                .HasColumnType("varchar")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.Description)
              .HasColumnType("text");

            builder.Property(x => x.IsActive);

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.DeletedAt);

            builder.HasMany(x => x.Measurements)
                .WithOne(x => x.FileType);
        }
    }
}
