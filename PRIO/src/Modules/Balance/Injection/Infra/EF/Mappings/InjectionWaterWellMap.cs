using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Balance.Injection.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Injection.Infra.EF.Mappings
{
    public class InjectionWaterWellMap : IEntityTypeConfiguration<InjectionWaterWell>
    {
        public void Configure(EntityTypeBuilder<InjectionWaterWell> builder)
        {
            builder.ToTable("Injection.InjectionWaterWell");

            builder.HasOne(x => x.WellValues)
               .WithOne(d => d.InjectionWaterWell)
               .HasForeignKey<InjectionWaterWell>("WellValuesId").IsRequired();
        }
    }
}
