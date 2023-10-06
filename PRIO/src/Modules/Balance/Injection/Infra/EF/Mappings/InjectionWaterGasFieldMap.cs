using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Balance.Injection.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Injection.Infra.EF.Mappings
{
    public class InjectionWaterGasFieldMap : IEntityTypeConfiguration<InjectionWaterGasField>
    {
        public void Configure(EntityTypeBuilder<InjectionWaterGasField> builder)
        {
            builder.ToTable("Injection.InjectionWaterGasField");


            builder.HasOne(x => x.BalanceField)
               .WithOne(d => d.InjectionWaterField)
               .HasForeignKey<InjectionWaterGasField>("BalanceFieldId")
               .IsRequired();

        }
    }
}
