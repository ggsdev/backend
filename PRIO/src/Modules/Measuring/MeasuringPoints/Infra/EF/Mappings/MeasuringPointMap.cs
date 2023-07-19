using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Models;

namespace PRIO.src.Modules.Measuring.MeasuringPoints.Infra.EF.Mappings
{
    public class MeasuringPointMap : IEntityTypeConfiguration<MeasuringPoint>
    {
        public void Configure(EntityTypeBuilder<MeasuringPoint> builder)
        {
            builder.ToTable
                    ("MeasuringPoints");

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

            builder.Property(x => x.Name)
               .HasColumnType("VARCHAR")
               .HasMaxLength(260)
               .IsRequired();

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.DeletedAt);

            builder.Property(x => x.IsActive);

            builder.HasOne(c => c.Installation).
                WithMany(u => u.MeasuringPoints)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
