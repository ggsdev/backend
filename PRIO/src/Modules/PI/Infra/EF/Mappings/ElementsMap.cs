using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.PI.Infra.EF.Models;

namespace PRIO.src.Modules.PI.Infra.EF.Mappings
{
    public class ElementsMap : IEntityTypeConfiguration<Elements>
    {
        public void Configure(EntityTypeBuilder<Elements> builder)
        {
            builder.ToTable("PI.Elements");

        }
    }
}