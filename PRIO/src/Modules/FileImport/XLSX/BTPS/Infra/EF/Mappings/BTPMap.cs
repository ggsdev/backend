using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Mappings
{
    public class BTPMap : IEntityTypeConfiguration<BTP>
    {
        public void Configure(EntityTypeBuilder<BTP> builder)
        {
            builder.ToTable("BTPs");

            builder.Property(x => x.Name)
              .HasColumnType("VARCHAR")
              .HasMaxLength(60)
              .IsRequired();

            builder.Property(x => x.Mutable)
              .IsRequired();

            builder.Property(x => x.Type)
              .HasColumnType("VARCHAR")
              .HasMaxLength(60)
              .IsRequired();

            builder.Property(x => x.FileContent)
             .HasColumnType("TEXT")
             .HasMaxLength(60)
             .IsRequired();

            builder.Property(x => x.CellPotencialOil)
             .HasColumnType("VARCHAR")
             .HasMaxLength(10)
             .IsRequired();

            builder.Property(x => x.CellPotencialGas)
             .HasColumnType("VARCHAR")
             .HasMaxLength(10)
             .IsRequired();

            builder.Property(x => x.CellPotencialWater)
             .HasColumnType("VARCHAR")
             .HasMaxLength(10)
             .IsRequired();

            builder.Property(x => x.CellInitialDate)
             .IsRequired();

            builder.Property(x => x.CellFinalDate)
             .IsRequired();

            builder.Property(x => x.CellDuration)
             .IsRequired();

            builder.Property(x => x.CellBTPNumber)
             .IsRequired();
        }
    }
}
