using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Mappings
{
    public class OilVolumeCalculationMap : IEntityTypeConfiguration<OilVolumeCalculation>
    {
        public void Configure(EntityTypeBuilder<OilVolumeCalculation> builder)
        {
            builder.ToTable("OilVoumeCalculations");

            builder.HasOne(x => x.Installation)
               .WithOne(d => d.OilVolumeCalculation)
               .HasForeignKey<OilVolumeCalculation>("InstallationId");
        }
    }
}
