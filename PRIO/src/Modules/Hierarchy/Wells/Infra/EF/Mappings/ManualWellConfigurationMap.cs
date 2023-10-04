using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Mappings
{
    public class ManualWellConfigurationMap : IEntityTypeConfiguration<ManualWellConfiguration>
    {
        public void Configure(EntityTypeBuilder<ManualWellConfiguration> builder)
        {
            builder.ToTable("ManualWellConfiguration.ManualWellConfigurations");

            builder.HasOne(x => x.Well)
                .WithOne(d => d.ManualWellConfiguration)
                .HasForeignKey<ManualWellConfiguration>("WellId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
