using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models;

namespace PRIO.Data.Mappings
{
    public class FieldMap : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder.ToTable("Fields");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
               .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.CodField)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.Acronym)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.Basin)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);


            builder.Property(x => x.State)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.Situation)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.WaterDepth)
                .HasColumnType("DECIMAL")
                .HasPrecision(6, 2);

            builder.Property(x => x.CorrectedArea)
               .HasColumnType("DECIMAL")
               .HasPrecision(6, 2);

            builder.Property(x => x.MainFluid)
               .HasColumnType("VARCHAR")
               .HasMaxLength(120);

            builder.Property(x => x.APIGradeOil)
                .HasColumnType("DECIMAL")
                .HasPrecision(5, 2);

            builder.Property(x => x.CalorificPowerGas)
                .HasColumnType("DECIMAL")
                .HasPrecision(8, 2);

            builder.Property(x => x.ContractNum)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.ContractOperator)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.ContractType)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.ContractTypeDescription)
                .HasColumnType("TEXT");

            builder.Property(x => x.Round)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.RoundDescription)
                .HasColumnType("TEXT");

            builder.Property(x => x.Original)
               .HasColumnType("VARCHAR")
               .HasMaxLength(120);

            builder.Property(x => x.Location)
               .HasColumnType("VARCHAR")
               .HasMaxLength(120);

            builder.Property(x => x.EnviromentDepth)
               .HasColumnType("VARCHAR")
               .HasMaxLength(120);

            builder.Property(x => x.DiscoveryDate)
               .HasColumnType("DATE");

            builder.Property(x => x.ProductionBeginning)
               .HasColumnType("DATE");

            builder.Property(x => x.Commerciality)
               .HasColumnType("DATE");

            builder.Property(x => x.ProductionFinishForecast)
               .HasColumnType("DATE");

            builder.Property(x => x.ProductionFinishDate)
               .HasColumnType("DATE");

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.IsActive);

            builder.Property(x => x.QtdWells);

            builder.Property(x => x.PreSaltWells);

            builder.HasOne(x => x.Cluster)
                .WithMany(c => c.Fields)
                .HasForeignKey("ClusterId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User).
                WithMany(u => u.Fields)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
