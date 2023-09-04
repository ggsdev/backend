using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.Productions.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.Productions.Infra.EF.Mappings
{
    public class GasMap : IEntityTypeConfiguration<Gas>
    {
        public void Configure(EntityTypeBuilder<Gas> builder)
        {
            builder.ToTable("Gases");

            builder.Property(x => x.LimitOperacionalBurn)
              .HasColumnType("DECIMAL")
              .HasPrecision(14, 5);

            builder.Property(x => x.ScheduledStopBurn)
                .HasColumnType("DECIMAL")
                .HasPrecision(14, 5);

            builder.Property(x => x.ForCommissioningBurn)
                .HasColumnType("DECIMAL")
                .HasPrecision(14, 5);

            builder.Property(x => x.VentedGas)
               .HasColumnType("DECIMAL")
               .HasPrecision(14, 5);

            builder.Property(x => x.WellTestBurn)
               .HasColumnType("DECIMAL")
               .HasPrecision(14, 5);

            builder.Property(x => x.EmergencialBurn)
               .HasColumnType("DECIMAL")
               .HasPrecision(14, 5);

            builder.Property(x => x.OthersBurn)
               .HasColumnType("DECIMAL")
               .HasPrecision(14, 5);

            builder.HasOne(x => x.GasLinear)
                .WithOne(d => d.Gas)
                .HasForeignKey<Gas>("GasLinearId")
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.GasDiferencial)
                .WithOne(d => d.Gas)
                .HasForeignKey<Gas>("GasDiferencialId")
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
