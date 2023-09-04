using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Models;

namespace PRIO.src.Modules.Hierarchy.Wells.Infra.EF.Mappings
{
    public class WellMap : IEntityTypeConfiguration<Well>
    {
        public void Configure(EntityTypeBuilder<Well> builder)
        {
            builder.ToTable("Wells");

            builder.Property(x => x.Name)
              .HasColumnType("VARCHAR")
              .HasMaxLength(60)
              .IsRequired();

            builder.Property(x => x.CodWell)
               .HasColumnType("VARCHAR")
               .HasMaxLength(60);

            builder.Property(e => e.WellOperatorName)
                .HasColumnType("VARCHAR")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(e => e.CodWellAnp)
                .HasColumnType("VARCHAR")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(e => e.CategoryAnp)
                .HasColumnType("VARCHAR")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(e => e.CategoryReclassificationAnp)
                .HasColumnType("VARCHAR")
                .HasMaxLength(60);

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

            builder.Property(e => e.CategoryOperator)
                .HasColumnType("VARCHAR")
                .HasMaxLength(60);

            builder.Property(e => e.Type)
                .HasColumnType("VARCHAR")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(e => e.WaterDepth)
                .HasColumnType("decimal")
                .HasPrecision(10, 2);

            builder.Property(e => e.ArtificialLift)
               .HasColumnType("VARCHAR")
               .HasMaxLength(60);

            builder.Property(e => e.Latitude4C)
              .HasColumnType("VARCHAR")
              .HasMaxLength(60)
              ;

            builder.Property(e => e.Longitude4C)
              .HasColumnType("VARCHAR")
              .HasMaxLength(60);

            builder.Property(e => e.LatitudeDD)
              .HasColumnType("VARCHAR")
              .HasMaxLength(60);
            builder.Property(e => e.LongitudeDD)
              .HasColumnType("VARCHAR")
              .HasMaxLength(60);

            builder.Property(e => e.TypeBaseCoordinate)
              .HasColumnType("VARCHAR")
              .HasMaxLength(60);

            builder.Property(e => e.CoordX)
               .HasColumnType("VARCHAR")
               .HasMaxLength(60);

            builder.Property(e => e.CoordY)
               .HasColumnType("VARCHAR")
               .HasMaxLength(60);

            builder.Property(e => e.DatumHorizontal)
               .HasColumnType("VARCHAR")
               .HasMaxLength(60);

            builder.Property(e => e.IsActive);

            builder.Property(e => e.CreatedAt)
                .HasColumnType("DATETIME");

            builder.Property(e => e.UpdatedAt)
                .HasColumnType("DATETIME");

            builder.Property(e => e.DeletedAt)
                .HasColumnType("DATETIME");

            builder.HasOne(x => x.User).
                WithMany(u => u.Wells)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(x => x.Field).
                WithMany(u => u.Wells)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
