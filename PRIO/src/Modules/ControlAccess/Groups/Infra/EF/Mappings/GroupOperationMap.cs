using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Groups.Infra.EF.Mappings
{
    public class GroupOperationMap : IEntityTypeConfiguration<GroupOperation>
    {
        public void Configure(EntityTypeBuilder<GroupOperation> builder)
        {

            builder.ToTable
                    ("AC.GroupOperations");

        }
    }
}
