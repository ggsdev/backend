using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Hierarchy.Installations.Infra.EF.Models;
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
               .HasForeignKey<Installation>("InstallationId");
        }
    }
}
