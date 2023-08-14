using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Mappings
{
    public class GasVolumeCalculationMap : IEntityTypeConfiguration<GasVolumeCalculation>
    {
        public void Configure(EntityTypeBuilder<GasVolumeCalculation> builder)
        {
            builder.ToTable("GasVolumeCalculations");

            builder.HasOne(x => x.Installation)
                .WithOne(d => d.GasVolumeCalculation)
                .HasForeignKey<GasVolumeCalculation>("InstallationId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
