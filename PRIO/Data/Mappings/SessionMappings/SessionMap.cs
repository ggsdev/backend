using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models.Users;

namespace PRIO.Data.Mappings.SessionMappings
{
    public class SessionMap : IEntityTypeConfiguration<Session>
    {

        public void Configure(EntityTypeBuilder<Session> builder)
        {

            builder.ToTable
                    ("Sessions");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Token)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255);

            builder.Property(x => x.ExpiresIn);

            builder.Property(x => x.IsActive);

            builder.HasOne(x => x.User)
                .WithOne(u => u.Session)
                .HasForeignKey<Session>("UserId")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
