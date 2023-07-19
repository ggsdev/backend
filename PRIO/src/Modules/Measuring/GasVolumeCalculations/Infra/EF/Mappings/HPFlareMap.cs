using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.GasVolumeCalculations.Infra.EF.Mappings
{
    public class HPFlareMap : IEntityTypeConfiguration<HPFlare>
    {
        public void Configure(EntityTypeBuilder<HPFlare> builder)
        {
            builder.ToTable("HpFlares");

            builder.Property(x => x.Name)
               .HasColumnType("VARCHAR")
               .HasMaxLength(260)
               .IsRequired();

            builder.HasOne(x => x.MeasuringPoint)
                .WithOne(d => d.HPFlare)
                .HasForeignKey<HPFlare>("MeasuringPointId");

            builder.HasOne(x => x.GasVolumeCalculation)
                .WithMany(d => d.HPFlares);
        }
    }
}
