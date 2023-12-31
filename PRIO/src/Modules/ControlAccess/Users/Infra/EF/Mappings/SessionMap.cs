﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;

namespace PRIO.src.Modules.ControlAccess.Users.Infra.EF.Mappings
{
    public class SessionMap : IEntityTypeConfiguration<Session>
    {

        public void Configure(EntityTypeBuilder<Session> builder)
        {

            builder.ToTable
                    ("System.Sessions");

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
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

        }
    }
}
