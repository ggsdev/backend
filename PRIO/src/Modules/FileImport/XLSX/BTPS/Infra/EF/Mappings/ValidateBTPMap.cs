using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Models;

namespace PRIO.src.Modules.FileImport.XLSX.BTPS.Infra.EF.Mappings
{
    public class ValidateBTPMap : IEntityTypeConfiguration<ValidateBTP>
    {
        public void Configure(EntityTypeBuilder<ValidateBTP> builder)
        {
            builder.ToTable("WellTest.Validates");

        }
    }
}
