using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models.Zones;

namespace PRIO.Data.Mappings.ZoneMappings
{
    public class ZoneHistoryMap : IEntityTypeConfiguration<ZoneHistory>
    {
        public void Configure(EntityTypeBuilder<ZoneHistory> builder)
        {
            {
            }
        }
    }
}