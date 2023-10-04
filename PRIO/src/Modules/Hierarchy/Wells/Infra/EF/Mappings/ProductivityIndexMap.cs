using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Mappings
{
    public class ProductivityIndexMap : IEntityTypeConfiguration<ProductivityIndex>
    {
        public void Configure(EntityTypeBuilder<ProductivityIndex> builder)
        {
            builder.ToTable("ManualWellConfiguration.ProductivityIndex");
        }
    }
}
