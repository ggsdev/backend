using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Balance.Injection.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Injection.Infra.EF.Mappings
{
    public class InjectionWaterFieldMap : IEntityTypeConfiguration<InjectionWaterField>
    {
        public void Configure(EntityTypeBuilder<InjectionWaterField> builder)
        {
            builder.ToTable("Injection.InjectionWaterField");


            builder.HasOne(x => x.BalanceField)
               .WithOne(d => d.InjectionWaterField)
               .HasForeignKey<InjectionWaterField>("BalanceFieldId").IsRequired();
        }
    }
}
