using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.PI.Infra.EF.Models;

namespace PRIO.src.Modules.PI.Infra.EF.Mappings
{
    public class InstancesMap : IEntityTypeConfiguration<Instance>
    {
        public void Configure(EntityTypeBuilder<Instance> builder)
        {
            builder.ToTable("PI.Instances");

        }
    }
}