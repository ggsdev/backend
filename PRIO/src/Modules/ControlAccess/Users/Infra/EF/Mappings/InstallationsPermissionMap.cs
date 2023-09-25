using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Mappings
{
    public class InstallationsPermissionMap : IEntityTypeConfiguration<InstallationsAccess>
    {
        public void Configure(EntityTypeBuilder<InstallationsAccess> builder)
        {

            builder.ToTable
                    ("AC.InstallationsAccess");

        }
    }
}
