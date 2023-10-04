using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Mappings
{
    public class BTPBase64Map : IEntityTypeConfiguration<BTPBase64>
    {
        public void Configure(EntityTypeBuilder<BTPBase64> builder)
        {
            builder.ToTable("WellTest.Bases64");


            builder.Property(x => x.FileContent)
             .HasColumnType("TEXT")
             .IsRequired();

            builder.Property(x => x.Filename)
             .HasColumnType("VARCHAR")
             .HasMaxLength(60)
             .IsRequired();

            builder.Property(x => x.Type)
             .HasColumnType("VARCHAR")
             .HasMaxLength(60)
             .IsRequired();


        }
    }
}
