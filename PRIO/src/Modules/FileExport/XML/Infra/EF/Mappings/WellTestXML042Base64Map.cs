using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.FileExport.XML.Infra.EF.Models;

namespace PRIO.src.Modules.FileExport.XML.Infra.EF.Mappings
{
    public class WellTestXML042Base64Map : IEntityTypeConfiguration<WellTestXML042Base64>
    {
        public void Configure(EntityTypeBuilder<WellTestXML042Base64> builder)
        {
            builder.ToTable("WellTests.XML042Base64");

        }
    }
}
