using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Mappings
{
    public class UserOperationMap : IEntityTypeConfiguration<UserOperation>
    {
        public void Configure(EntityTypeBuilder<UserOperation> builder)
        {
            builder.ToTable("UserOperations");

            builder.Property(x => x.OperationName)
                .HasColumnType("varchar")
                .HasMaxLength(120);

            builder.Property(x => x.GroupName)
                .HasColumnType("varchar")
                .HasMaxLength(120);

            builder.HasOne(x => x.UserPermission)
                .WithMany(x => x.UserOperation)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.GlobalOperation)
                .WithMany(x => x.UserOperations)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
