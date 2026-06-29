using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.id);

            builder.Property(u => u.Firstname)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Lastname)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Displayname)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(500);
           
            builder.Property(u => u.Isemailverified)
                .HasDefaultValue(false);

            builder.Property(u => u.Istwofactorenabled)
                .HasDefaultValue(false);

            builder.Property(u => u.Accountstatus)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Phonenumber)
                .HasMaxLength(20);

            builder.Property(u => u.Isphonenumberverified)
             .HasDefaultValue(false);

            builder.Property(u => u.Lastlogin)
           .HasColumnType("timestamp with time zone")
           .IsRequired(false);

            builder.Property(u => u.Lastotp)
                .HasMaxLength(10);

            builder.Property(u => u.Failedloginattempts)
             .HasDefaultValue(false);

            builder.Property(u => u.Passwordchangedat)
            .HasColumnType("timestamp with time zone")
            .IsRequired(false);

            builder.Property(d => d.createddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(o => o.isdeleted)
             .HasDefaultValue(false);

            builder.HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.UserPractices)
                .WithOne(up => up.User)
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
