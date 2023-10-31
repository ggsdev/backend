using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.FileExport.Templates.Infra.EF.Models;

namespace PRIO.src.Modules.FileExport.Templates.Infra.EF.Mappings
{
    public class TemplateMap : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.ToTable("FileExport.Templates");

            builder.Property(x => x.FileContent)
                .HasColumnType("TEXT")
                .IsRequired();

            builder.Property(x => x.FileName)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(120)
                    .IsRequired();

            builder.Property(x => x.FileExtension)
                    .HasColumnType("CHAR")
                    .HasMaxLength(4)
                    .IsRequired();

            builder.Property(x => x.Description)
                    .HasColumnType("TEXT");

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.DeletedAt);

            builder.Property(x => x.IsActive);

            builder.Property(x => x.TypeFile)
                .HasConversion<int>();
        }
    }
}
