using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Balance.Balance.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Balance.Infra.EF.Mappings
{
    public class InstallationsBalanceMap : IEntityTypeConfiguration<InstallationsBalance>
    {
        public void Configure(EntityTypeBuilder<InstallationsBalance> builder)
        {
            builder.ToTable("Balance.InstallationsBalance");

        }
    }
}
