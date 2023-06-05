using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models.Wells;

namespace PRIO.Data.Mappings.WellMappings
{
    public class WellHistoryMap : IEntityTypeConfiguration<WellHistory>
    {
        public void Configure(EntityTypeBuilder<WellHistory> builder)
        {
            builder.ToTable("Wells");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(e => e.NameOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150);

            builder.Property(e => e.WellOperatorName)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150);

            builder.Property(e => e.WellOperatorNameOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150);

            builder.Property(e => e.CodWell)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150);

            builder.Property(e => e.CodWellOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150);

            builder.Property(e => e.CodWellAnp)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150);

            builder.Property(e => e.CodWellAnpOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150);

            builder.Property(e => e.CategoryAnp)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150);

            builder.Property(e => e.CategoryAnpOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150);

            builder.Property(e => e.CategoryReclassificationAnp)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150);

            builder.Property(e => e.CategoryReclassificationAnpOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150);

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

            builder.Property(x => x.DescriptionOld)
                .HasColumnType("TEXT");

            builder.Property(e => e.CategoryOperator)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150);

            builder.Property(e => e.CategoryOperatorOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150);

            builder.Property(e => e.Type)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150);

            builder.Property(e => e.TypeOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150);

            builder.Property(e => e.WaterDepth)
                .HasColumnType("float")
                .HasPrecision(10, 2);

            builder.Property(e => e.WaterDepthOld)
                .HasColumnType("float")
                .HasPrecision(10, 2);

            builder.Property(e => e.TopOfPerforated)
               .HasColumnType("float")
                .HasPrecision(10, 2);

            builder.Property(e => e.TopOfPerforatedOld)
               .HasColumnType("float")
                .HasPrecision(10, 2);

            builder.Property(e => e.BaseOfPerforated)
               .HasColumnType("float")
                .HasPrecision(10, 2);

            builder.Property(e => e.BaseOfPerforatedOld)
               .HasColumnType("float")
                .HasPrecision(10, 2);

            builder.Property(e => e.ArtificialLift)
               .HasColumnType("VARCHAR")
               .HasMaxLength(150);

            builder.Property(e => e.ArtificialLiftOld)
               .HasColumnType("VARCHAR")
               .HasMaxLength(150);

            builder.Property(e => e.Latitude4C)
              .HasColumnType("VARCHAR")
              .HasMaxLength(150);

            builder.Property(e => e.Latitude4COld)
              .HasColumnType("VARCHAR")
              .HasMaxLength(150);

            builder.Property(e => e.Longitude4C)
              .HasColumnType("VARCHAR")
              .HasMaxLength(150);

            builder.Property(e => e.Longitude4COld)
              .HasColumnType("VARCHAR")
              .HasMaxLength(150);

            builder.Property(e => e.LatitudeDD)
              .HasColumnType("VARCHAR")
              .HasMaxLength(150);

            builder.Property(e => e.LatitudeDDOld)
              .HasColumnType("VARCHAR")
              .HasMaxLength(150);

            builder.Property(e => e.LongitudeDD)
              .HasColumnType("VARCHAR")
              .HasMaxLength(150);

            builder.Property(e => e.LongitudeDDOld)
              .HasColumnType("VARCHAR")
              .HasMaxLength(150);

            builder.Property(e => e.TypeBaseCoordinate)
              .HasColumnType("VARCHAR")
              .HasMaxLength(150);

            builder.Property(e => e.TypeBaseCoordinateOld)
              .HasColumnType("VARCHAR")
              .HasMaxLength(150);

            builder.Property(e => e.CoordX)
               .HasColumnType("VARCHAR")
               .HasMaxLength(150);

            builder.Property(e => e.CoordXOld)
               .HasColumnType("VARCHAR")
               .HasMaxLength(150);

            builder.Property(e => e.CoordY)
               .HasColumnType("VARCHAR")
               .HasMaxLength(150);

            builder.Property(e => e.CoordYOld)
               .HasColumnType("VARCHAR")
               .HasMaxLength(150);

            builder.Property(e => e.IsActive);

            builder.Property(e => e.IsActiveOld);

            builder.Property(e => e.CreatedAt)
                .HasColumnType("DATETIME");

            builder.Property(x => x.FieldOld)
             .HasColumnType("UniqueIdentifier")
             .HasMaxLength(120);

            builder.HasOne(x => x.User).
                WithMany(u => u.WellHistories)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(x => x.Well).
                WithMany(u => u.WellHistories)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(x => x.Field).
                WithMany(u => u.WellHistories)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
