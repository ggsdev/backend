using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.PI.Infra.EF.Models;

namespace PRIO.src.Modules.PI.Infra.EF.Mappings
{
    public class AttributesMap : IEntityTypeConfiguration<Attributes>
    {
        public void Configure(EntityTypeBuilder<Attributes> builder)
        {
            builder.ToTable("PI.Attributes");

        }
    }
}