using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.PI.Infra.EF.Models;

namespace PRIO.src.Modules.PI.Infra.EF.Mappings
{
    public class WellsValuesMap : IEntityTypeConfiguration<WellsValues>
    {
        public void Configure(EntityTypeBuilder<WellsValues> builder)
        {
            builder.ToTable("PI.WellsValues");
        }
    }
}
