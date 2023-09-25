using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.FileImport.XML.NFSMS.Infra.EF.Models;

namespace PRIO.src.Modules.FileImport.XML.NFSMS.Infra.EF.Mappings
{
    public class NFSMHistoryMap : IEntityTypeConfiguration<NFSMHistory>
    {
        public void Configure(EntityTypeBuilder<NFSMHistory> builder)
        {
            builder.ToTable("Measurement.NFSMImportHistories");

            builder.Property(x => x.FileAcronym)
                .HasColumnType("char")
                .HasMaxLength(3);

            builder.Property(x => x.FileType)
               .HasColumnType("char")
                .HasMaxLength(3);

            builder.Property(x => x.FileName)
                .HasColumnType("varchar")
                .HasMaxLength(800);

            builder.Property(x => x.TypeOperation)
                .HasMaxLength(20);

            builder.HasOne(x => x.ImportedBy)
                 .WithMany(x => x.NFSMImportedHistories)
                 .OnDelete(DeleteBehavior.NoAction)
                 .IsRequired();
        }
    }
}