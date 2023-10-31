using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.FileExport.XML.Infra.EF.Models;

namespace PRIO.src.Modules.FileExport.XML.Infra.EF.Mappings
{
    public class WellEventXML042Base64Map : IEntityTypeConfiguration<WellEventXML042Base64>
    {
        public void Configure(EntityTypeBuilder<WellEventXML042Base64> builder)
        {
            builder.ToTable("Event.XML042Base64");

        }
    }
}
