using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.FileImport.XML.Measuring.Infra.EF.Models;

namespace PRIO.src.Modules.FileImport.XML.Measuring.Infra.EF.Mappings
{
    public class ImportedFileMap : IEntityTypeConfiguration<ImportedFile>
    {
        public void Configure(EntityTypeBuilder<ImportedFile> builder)
        {
            builder.ToTable("ImportedFiles");

            builder.Property(x => x.Content)
                .HasColumnType("TEXT")
                .IsRequired();

            builder.Property(x => x.Description)
               .HasColumnType("TEXT");

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.DeletedAt);

            builder.Property(x => x.IsActive);

            builder.Property(x => x.FileType)
                .HasColumnType("varchar")
                .HasMaxLength(3)
                .IsRequired();
        }
    }
}
