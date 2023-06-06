using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.Models.Users;

namespace PRIO.Data.Mappings.UserMappings
{
    public class UserHistoryMap : IEntityTypeConfiguration<UserHistory>
    {
        public void Configure(EntityTypeBuilder<UserHistory> builder)
        {

            builder.ToTable
                    ("Users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(x => x.NameOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.Email)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.EmailOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(150);

            builder.Property(x => x.Password)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(90);
            
            builder.Property(x => x.PasswordOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(90);
                 
            builder.Property(x => x.Type)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(90);
                 
            builder.Property(x => x.TypeOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(90);

            builder.Property(x => x.Username)
                .IsRequired().HasColumnType("VARCHAR")
                .HasMaxLength(120);
            
            builder.Property(x => x.UsernameOld)
                .HasColumnType("VARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.Description)
                .HasColumnType("TEXT");

            builder.Property(x => x.DescriptionOld)
                .HasColumnType("TEXT");

            builder.Property(x => x.CreatedAt);

            builder.Property(x => x.IsActive); 
            
            builder.Property(x => x.IsActiveOld);

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.TypeOperation)
                .HasColumnType("VARCHAR")
                .HasMaxLength(20)
                .IsRequired();

            builder.HasOne(c => c.User).
               WithMany(u => u.UserHistories)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();
        }
    }
}
