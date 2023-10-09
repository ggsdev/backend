using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Balance.Injection.Infra.EF.Models;

namespace PRIO.src.Modules.Balance.Injection.Infra.EF.Mappings
{
    public class WellSensorMap : IEntityTypeConfiguration<WellSensor>
    {
        public void Configure(EntityTypeBuilder<WellSensor> builder)
        {
            builder.ToTable("Injection.WellSensors");

            builder.HasOne(x => x.WellValues)
               .WithOne(d => d.WellSensor)
               .HasForeignKey<WellSensor>("WellValuesId").IsRequired();
        }
    }
}
