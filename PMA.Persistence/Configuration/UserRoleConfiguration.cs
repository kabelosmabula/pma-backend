using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("userroles");

            builder.HasKey(ur => new { ur.UserId, ur.RoleId, ur.PracticeId });

            builder.Property(ur => ur.Isactive)
           .IsRequired();

            builder.Property(d => d.createddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(o => o.isdeleted)
             .HasDefaultValue(false);

            builder.HasOne(ur => ur.User)
                   .WithMany(u => u.UserRoles)
                   .HasForeignKey(ur => ur.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ur => ur.Role)
                   .WithMany()
                   .HasForeignKey(ur => ur.RoleId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ur => ur.Practice)
                   .WithMany()
                   .HasForeignKey(ur => ur.PracticeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(ur => new { ur.UserId, ur.RoleId, ur.PracticeId })
            .IsUnique();
        }
    }
}
