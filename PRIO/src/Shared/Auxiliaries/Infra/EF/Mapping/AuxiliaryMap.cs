using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Shared.Auxiliaries.Infra.EF.Models;

namespace PRIO.src.Shared.Auxiliaries.Infra.EF.Mapping
{
    public class AuxiliaryMap : IEntityTypeConfiguration<Auxiliary>
    {
        public void Configure(EntityTypeBuilder<Auxiliary> builder)
        {
            builder.ToTable
                    ("System.Auxiliary");

        }
    }
}
