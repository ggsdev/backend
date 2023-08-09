using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.FileImport.XML.NFSMS.Infra.EF.Models;

namespace PRIO.src.Modules.FileImport.XML.NFSMS.Infra.EF.Mappings
{
    public class NFSMMap : IEntityTypeConfiguration<NFSM>
    {
        public void Configure(EntityTypeBuilder<NFSM> builder)
        {
            builder.ToTable("NFSMs");

            builder.Property(x => x.CodeFailure)
                .HasColumnType("varchar")
                .HasMaxLength(60);

            builder.Property(x => x.DescriptionFailure)
               .HasColumnType("text");

            builder.Property(x => x.Action)
                .HasColumnType("varchar")
                .HasMaxLength(1000);

            builder.Property(x => x.Methodology)
                .HasColumnType("varchar")
                .HasMaxLength(1000);

            builder.Property(x => x.TypeOfFailure)
                .IsRequired();

            builder.HasOne(x => x.Installation)
                  .WithMany(x => x.NFSMs)
                  .OnDelete(DeleteBehavior.NoAction)
                  .IsRequired();

            builder.HasOne(x => x.ImportHistory)
                  .WithOne(x => x.NFSM)
                  .HasForeignKey<NFSM>("ImportId")
                  .OnDelete(DeleteBehavior.NoAction)
                  .IsRequired();

            builder.HasOne(x => x.MeasuringPoint)
                 .WithMany(x => x.NFSMs)
                 .OnDelete(DeleteBehavior.NoAction)
                 .IsRequired();
        }
    }

    public class NFSMsProductionsMap : IEntityTypeConfiguration<NFSMsProductions>
    {
        public void Configure(EntityTypeBuilder<NFSMsProductions> builder)
        {
            builder.ToTable("NFSMsProductions");

            builder.Property(x => x.VolumeAfter)
                .HasColumnType("decimal")
                .HasPrecision(10, 5);

            builder.Property(x => x.VolumeBefore)
                .HasColumnType("decimal")
                .HasPrecision(10, 5);

            builder.HasOne(x => x.Production)
                 .WithMany(x => x.NFSMs)
                 .OnDelete(DeleteBehavior.NoAction)
                 .IsRequired();

            builder.HasOne(x => x.NFSM)
                 .WithMany(x => x.Productions)
                 .OnDelete(DeleteBehavior.NoAction)
                 .IsRequired();
        }
    }
}
