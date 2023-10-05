using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Balance.Balance.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Balance.Infra.EF.Mappings
{
    public class UEPsBalanceMap : IEntityTypeConfiguration<UEPsBalance>
    {
        public void Configure(EntityTypeBuilder<UEPsBalance> builder)
        {
            builder.ToTable("Balance.UEPsBalance");
        }
    }
}
