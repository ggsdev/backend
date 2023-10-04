using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Mappings
{
    public class InjectivityIndexMap : IEntityTypeConfiguration<InjectivityIndex>
    {
        public void Configure(EntityTypeBuilder<InjectivityIndex> builder)
        {
            builder.ToTable("ManualWellConfiguration.InjectivityIndex");
        }
    }
}
