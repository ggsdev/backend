using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models;

namespace PRIO.Data.Mappings
{
    public class InstallationMap : IEntityTypeConfiguration<Installation>
    {
        public void Configure(EntityTypeBuilder<Installation> builder)
        {
            builder.ToTable("Installations");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.CodInstallation)
               .HasColumnType("VARCHAR")
               .HasMaxLength(120);

            builder.Property(x => x.Acronym)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.Operator)
               .HasColumnType("VARCHAR")
               .HasMaxLength(120);

            builder.Property(x => x.Owner)
               .HasColumnType("VARCHAR")
               .HasMaxLength(120);

            builder.Property(x => x.Type)
               .HasColumnType("VARCHAR")
               .HasMaxLength(120);

            builder.Property(x => x.Environment)
               .HasColumnType("VARCHAR")
               .HasMaxLength(120);

            builder.Property(x => x.WaterDepth)
               .HasColumnType("DECIMAL")
               .HasPrecision(10, 2);

            builder.Property(x => x.State)
               .HasColumnType("VARCHAR")
               .HasMaxLength(2);

            builder.Property(x => x.City)
              .HasColumnType("VARCHAR")
              .HasMaxLength(120);

            builder.Property(x => x.FieldService)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.Latitude)
                .HasColumnType("VARCHAR")
                .HasMaxLength(30);

            builder.Property(x => x.Longitude)
                .HasColumnType("VARCHAR")
                .HasMaxLength(30);

            builder.Property(x => x.GasProcessing)
                .HasColumnType("DECIMAL")
                .HasPrecision(10, 2);

            builder.Property(x => x.OilProcessing)
                .HasColumnType("DECIMAL")
                .HasPrecision(10, 2);

            builder.Property(x => x.BeginningValidity)
                .HasColumnType("DATE");

            builder.Property(x => x.InclusionDate)
                .HasColumnType("DATE");

            builder.Property(x => x.PsmQty)
                .HasColumnType("int");

            builder.Property(x => x.Situation)
                .HasColumnType("bit");

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.IsActive);

            builder.HasOne(x => x.Field)
                .WithMany(f => f.Installations)
                .HasForeignKey("FieldId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
               .WithMany(u => u.Installations)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
