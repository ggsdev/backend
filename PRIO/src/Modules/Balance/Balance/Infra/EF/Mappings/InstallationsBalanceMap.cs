using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Balance.Balance.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Balance.Infra.EF.Mappings
{
    public class InstallationsBalanceMap : IEntityTypeConfiguration<InstallationsBalance>
    {
        public void Configure(EntityTypeBuilder<InstallationsBalance> builder)
        {
            builder.ToTable("Balance.InstallationsBalance");

            builder.Property(x => x.TotalWaterCaptured)
              .HasColumnType("DECIMAL")
              .HasPrecision(38, 16);

            builder.Property(x => x.TotalWaterProduced)
               .HasColumnType("DECIMAL")
               .HasPrecision(38, 16);

            builder.Property(x => x.TotalWaterDisposal)
               .HasColumnType("DECIMAL")
               .HasPrecision(38, 16);

            builder.Property(x => x.TotalWaterInjected)
               .HasColumnType("DECIMAL")
               .HasPrecision(38, 16);

            builder.Property(x => x.TotalWaterInjectedRS)
               .HasColumnType("DECIMAL")
               .HasPrecision(38, 16);

            builder.Property(x => x.DischargedSurface)
              .HasColumnType("DECIMAL")
              .HasPrecision(38, 16);

            builder.Property(x => x.TotalWaterReceived)
              .HasColumnType("DECIMAL")
              .HasPrecision(38, 16);

            builder.Property(x => x.TotalWaterTransferred)
              .HasColumnType("DECIMAL")
              .HasPrecision(38, 16);
        }
    }
}
