using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Prio_BackEnd.Models;

namespace Prio_BackEnd.Data.Mappings
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
                .HasMaxLength(256);
           
            builder.Property(x => x.Acronym)
                .HasColumnType("varchar")
                .HasMaxLength(60);

            builder.Property(x => x.QtdColumns)
                .HasColumnType("int");

            builder.Property(x => x.Structure)
               .HasColumnType("text");

            builder.Property(x => x.IsActive)
                .HasColumnType("bit")
                .HasDefaultValue(true);

            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.Description)
               .HasColumnType("text");

        }
    }
}
