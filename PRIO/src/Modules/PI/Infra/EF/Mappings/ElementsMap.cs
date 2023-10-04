using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.PI.Infra.EF.Models;

namespace PRIO.src.Modules.PI.Infra.EF.Mappings
{
    public class ElementsMap : IEntityTypeConfiguration<Element>
    {
        public void Configure(EntityTypeBuilder<Element> builder)
        {
            builder.ToTable("PI.Elements");

            builder.HasIndex(x => x.WebId)
                .IsUnique();
        }
    }
}