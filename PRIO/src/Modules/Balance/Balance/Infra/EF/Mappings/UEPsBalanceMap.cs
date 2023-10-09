using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Balance.Balance.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Balance.Infra.EF.Mappings
{
    public class UEPsBalanceMap : IEntityTypeConfiguration<UEPsBalance>
    {
        public void Configure(EntityTypeBuilder<UEPsBalance> builder)
        {
            builder.ToTable("Balance.UEPsBalance");

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

            builder.Property(x => x.TotalWaterReceived)
              .HasColumnType("DECIMAL")
              .HasPrecision(38, 16);

            builder.Property(x => x.DischargedSurface)
              .HasColumnType("DECIMAL")
              .HasPrecision(38, 16);

            builder.Property(x => x.TotalWaterTransferred)
              .HasColumnType("DECIMAL")
              .HasPrecision(38, 16);

            builder.HasOne(x => x.Uep)
                .WithMany(x => x.UepBalances)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
