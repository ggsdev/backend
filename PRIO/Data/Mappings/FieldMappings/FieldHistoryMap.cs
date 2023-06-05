using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models.Fields;

namespace PRIO.Data.Mappings.FieldMappings
{
    public class FieldHistoryMap : IEntityTypeConfiguration<FieldHistory>
    {
        public void Configure(EntityTypeBuilder<FieldHistory> builder)
        {
            builder.ToTable("FieldHistories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
               .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.NameOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.CodField)
                .HasColumnType("VARCHAR")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.CodFieldOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(10);

            builder.Property(x => x.State)
               .HasColumnType("VARCHAR")
               .HasMaxLength(120);

            builder.Property(x => x.StateOld)
               .HasColumnType("VARCHAR")
               .HasMaxLength(120);

            builder.Property(x => x.Basin)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);


            builder.Property(x => x.BasinOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.Location)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.LocationOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.InstallationOld)
                .HasColumnType("UniqueIdentifier")
                .HasMaxLength(120);

            builder.Property(x => x.Description)
               .HasColumnType("TEXT");

            builder.Property(x => x.DescriptionOld)
               .HasColumnType("TEXT");

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.UpdatedAt);

            builder.Property(x => x.IsActive);

            builder.Property(x => x.IsActiveOld);

            builder.HasOne(x => x.Field)
                .WithMany(c => c.FieldHistories)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(x => x.Installation)
                .WithMany(c => c.FieldHistories)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(x => x.User).
                WithMany(u => u.FieldHistories)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired();
        }
    }
}
