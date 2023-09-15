using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Mappings
{
    public class WellTestMap : IEntityTypeConfiguration<WellTests>
    {
        public void Configure(EntityTypeBuilder<WellTests> builder)
        {
            builder.ToTable("WellTests");

            builder.Property(x => x.Filename)
             .HasColumnType("VARCHAR")
             .HasMaxLength(60)
             .IsRequired();

            builder.Property(x => x.Type)
             .HasColumnType("VARCHAR")
             .HasMaxLength(60)
             .IsRequired();

            builder.Property(x => x.PotencialOil)
             .HasColumnType("decimal")
             .HasPrecision(15, 5)
             .IsRequired();

            builder.Property(x => x.PotencialOilPerHour)
             .HasColumnType("decimal")
             .HasPrecision(15, 5)
             .IsRequired();

            builder.Property(x => x.PotencialLiquid)
            .HasColumnType("decimal")
            .HasPrecision(15, 5)
            .IsRequired();

            builder.Property(x => x.PotencialLiquidPerHour)
           .HasColumnType("decimal")
           .HasPrecision(15, 5)
           .IsRequired();

            builder.Property(x => x.PotencialGas)
             .HasColumnType("decimal")
                .HasPrecision(15, 5)
             .IsRequired();

            builder.Property(x => x.RGO)
             .HasColumnType("decimal")
                .HasPrecision(15, 5)
             .IsRequired();

            builder.Property(x => x.BSW)
             .HasColumnType("decimal")
                .HasPrecision(15, 5)
             .IsRequired();

            builder.Property(x => x.PotencialGasPerHour)
             .HasColumnType("decimal")
             .HasPrecision(15, 5)
             .IsRequired();

            builder.Property(x => x.PotencialWater)
             .HasColumnType("decimal")
             .HasPrecision(15, 5)
             .IsRequired();

            builder.Property(x => x.PotencialWaterPerHour)
             .HasColumnType("decimal")
             .HasPrecision(15, 5)
             .IsRequired();

            builder.Property(x => x.InitialDate)
             .HasColumnType("VARCHAR")
             .HasMaxLength(60)
             .IsRequired();


            builder.Property(x => x.FinalDate)
             .HasColumnType("VARCHAR")
             .HasMaxLength(60)
             .IsRequired();

            builder.Property(x => x.Duration)
             .HasColumnType("VARCHAR")
             .HasMaxLength(60)
             .IsRequired();

            builder.Property(x => x.ApplicationDate)
             .HasColumnType("VARCHAR")
             .HasMaxLength(60);

            builder.Property(x => x.IsValid)
             .IsRequired();

            builder.Property(x => x.BTPNumber)
             .HasColumnType("VARCHAR")
             .HasMaxLength(60)
             .IsRequired();

            builder.HasOne(x => x.Well)
               .WithMany(u => u.WellTests)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();

            builder.HasOne(x => x.BTPBase64)
               .WithOne(d => d.WellTest)
               .HasForeignKey<WellTests>("BTPBase64Id").IsRequired();
        }
    }
}
