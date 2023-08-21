using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Fields.Infra.EF.Mappings
{
    public class FieldFRsMap : IEntityTypeConfiguration<FieldFR>
    {
        public void Configure(EntityTypeBuilder<FieldFR> builder)
        {
            builder.ToTable("FieldsFRs");

            builder.Property(x => x.FROil)
               .HasColumnType("decimal")
               .HasPrecision(4, 2);

            builder.Property(x => x.FRGas)
               .HasColumnType("decimal")
               .HasPrecision(4, 2);

            builder.Property(x => x.ProductionInField)
               .HasColumnType("decimal")
               .HasPrecision(10, 5);


            builder.HasOne(x => x.DailyProduction)
               .WithMany(x => x.FieldsFR)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();

            builder.HasOne(x => x.Field)
               .WithMany(x => x.FRs)
               .IsRequired();

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.DeletedAt);

            builder.Property(x => x.IsActive);
        }
    }
}
