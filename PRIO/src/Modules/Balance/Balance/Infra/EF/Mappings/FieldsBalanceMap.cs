using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Balance.Balance.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Balance.Infra.EF.Mappings
{
    public class FieldsBalanceMap : IEntityTypeConfiguration<FieldsBalance>
    {
        public void Configure(EntityTypeBuilder<FieldsBalance> builder)
        {
            builder.ToTable("Balance.FieldsBalance");

            builder.HasOne(x => x.FieldProduction)
               .WithOne(d => d.FieldsBalance)
               .HasForeignKey<FieldsBalance>("FieldProductionId").IsRequired();
        }
    }
}
