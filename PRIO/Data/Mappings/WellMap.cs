using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Prio_BackEnd.Models;


namespace Prio_BackEnd.Data.Mappings
{
    public class WellMap : IEntityTypeConfiguration<Well>
    {
        public void Configure(EntityTypeBuilder<Well> builder)
        {
            builder.ToTable("Wells");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();


            builder.Property(e => e.IsActive)
                .HasDefaultValue(true);

            builder.Property(e => e.CreatedAt)
                .HasColumnType("DATETIME");

            builder.Property(e => e.UpdatedAt)
                .HasColumnType("DATETIME");


            builder.Property(e => e.NameAnp)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(e => e.RegisterNum)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);


            builder.Property(e => e.WellOperatorName)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(e => e.FieldName)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(e => e.CompanyCodOperator)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(e => e.Basin)
               .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(e => e.State)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(e => e.Category)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(e => e.CompletionDate)
                .HasColumnType("DATE");

            builder.Property(e => e.DrillingFinishDate)
                .HasColumnType("DATE");

            builder.Property(e => e.DrillingStartDate)
                .HasColumnType("DATE");

            builder.Property(e => e.Latitude)
                .HasColumnType("VARCHAR")
                .HasMaxLength(60);

            builder.Property(e => e.Longitude)
                .HasColumnType("VARCHAR")
                .HasMaxLength(60);

            builder.Property(e => e.Location)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(e => e.WaterDepth)
                .HasColumnType("DECIMAL")
                .HasPrecision(10, 2);

            builder.Property(e => e.SounderDepth)
                .HasColumnType("DECIMAL")
                .HasPrecision(10, 2);

            builder.Property(e => e.InstallationName)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(e => e.EnviromentProduction)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(e => e.Block)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(e => e.ClusterName)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(e => e.CodInstallation)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(e => e.ReservoirName)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(e => e.ProductionByReservoir)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);

            builder.Property(e => e.CompletionName)
                .HasColumnType("VARCHAR")
                .HasMaxLength(255);


            builder.Property(e => e.TopOfPerforated)
               .HasColumnType("VARCHAR")
               .HasMaxLength(255);

            builder.Property(e => e.BaseOfPerforated)
               .HasColumnType("VARCHAR")
               .HasMaxLength(255);

            builder.Property(e => e.Type)
               .HasColumnType("VARCHAR")
               .HasMaxLength(255);

            builder.Property(e => e.MD)
               .HasColumnType("VARCHAR")
               .HasMaxLength(255);

            builder.Property(e => e.TVD)
               .HasColumnType("VARCHAR")
               .HasMaxLength(255);

            builder.Property(e => e.ArtificialElevation)
               .HasColumnType("VARCHAR")
               .HasMaxLength(255);

            builder.Property(e => e.CoordX)
               .HasColumnType("VARCHAR")
               .HasMaxLength(255);

            builder.Property(e => e.CoordY)
               .HasColumnType("VARCHAR")
               .HasMaxLength(255);

            builder.Property(e => e.FieldCod)
               .HasColumnType("VARCHAR")
               .HasMaxLength(255);

            builder.Property(e => e.CurrentSituation)
               .HasColumnType("VARCHAR")
               .HasMaxLength(255);

            builder.HasOne(x => x.User).
                WithMany(u => u.Wells)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Completion)
                .WithMany(c => c.Wells)
                .HasForeignKey("CompletionId")
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
