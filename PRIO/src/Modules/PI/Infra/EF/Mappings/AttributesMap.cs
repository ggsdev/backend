using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PRIO.src.Modules.PI.Infra.EF.Mappings
{
    public class AttributesMap : IEntityTypeConfiguration<Models.Attribute>
    {
        public void Configure(EntityTypeBuilder<Models.Attribute> builder)
        {
            builder.ToTable("PI.Attributes");
        }
    }
}