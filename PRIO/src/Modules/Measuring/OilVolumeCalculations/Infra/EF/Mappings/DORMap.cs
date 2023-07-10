using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.OilVolumeCalculations.Infra.EF.Mappings
{
    public class DORMap : IEntityTypeConfiguration<DOR>
    {
        public void Configure(EntityTypeBuilder<DOR> builder)
        {
            builder.ToTable("DORs");

            builder.HasOne(x => x.MeasuringPoint)
                .WithOne(d => d.DOR)
                .HasForeignKey<DOR>("MeasuringPointId");

            builder.HasOne(x => x.OilVolumeCalculation)
                .WithMany(d => d.DORs);
        }
    }

}
