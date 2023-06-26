using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.ControlAccess.Operations.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Operations.Infra.EF.Mappings
{
    public class GlobalOperationMap : IEntityTypeConfiguration<GlobalOperation>
    {
        public void Configure(EntityTypeBuilder<GlobalOperation> builder)
        {
            builder.ToTable("GlobalOperations");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Method)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.DeletedAt);

            builder.Property(x => x.IsActive);
        }
    }
}
