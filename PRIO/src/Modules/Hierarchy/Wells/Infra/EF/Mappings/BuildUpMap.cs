using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Mappings
{
    public class BuildUpMap : IEntityTypeConfiguration<BuildUp>
    {
        public void Configure(EntityTypeBuilder<BuildUp> builder)
        {
            builder.ToTable("ManualWellConfiguration.BuildUps");

        }
    }
}
