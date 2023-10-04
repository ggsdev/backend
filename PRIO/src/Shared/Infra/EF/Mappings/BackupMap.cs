using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Shared.Infra.EF.Models;

namespace PRIO.src.Shared.Infra.EF.Mappings
{
    public class BackupMap : IEntityTypeConfiguration<Backup>
    {
        public void Configure(EntityTypeBuilder<Backup> builder)
        {
            builder.ToTable
                    ("System.Backups");

            builder.HasIndex(x => x.date)
                .IsUnique();
        }
    }
}
