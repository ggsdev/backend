using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Balance.Injection.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Injection.Infra.EF.Mappings
{
    public class InjectionGasWellMap : IEntityTypeConfiguration<InjectionGasWell>
    {
        public void Configure(EntityTypeBuilder<InjectionGasWell> builder)
        {
            builder.ToTable("Injection.InjectionGasWell");

            builder.HasOne(x => x.WellValues)
               .WithOne(d => d.InjectionGasWell)
               .HasForeignKey<InjectionGasWell>("WellValuesId")
               .IsRequired();
        }
    }
}
