using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Balance.Balance.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Balance.Infra.EF.Mappings
{
    public class FieldsBalanceMap : IEntityTypeConfiguration<FieldsBalance>
    {
        public void Configure(EntityTypeBuilder<FieldsBalance> builder)
        {
            builder.ToTable("Balance.FieldsBalance");

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

            builder.Property(x => x.DischargedSurface)
              .HasColumnType("DECIMAL")
              .HasPrecision(38, 16);

            builder.Property(x => x.TotalWaterInjectedRS)
               .HasColumnType("DECIMAL")
               .HasPrecision(38, 16);

            builder.Property(x => x.TotalWaterReceived)
              .HasColumnType("DECIMAL")
              .HasPrecision(38, 16);

            builder.Property(x => x.TotalWaterTransferred)
              .HasColumnType("DECIMAL")
              .HasPrecision(38, 16);

            builder.HasOne(x => x.Field)
                .WithMany(x => x.FieldBalances)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.FieldProduction)
               .WithOne(d => d.FieldsBalance)
               .HasForeignKey<FieldsBalance>("FieldProductionId").IsRequired();

        }
    }
}
