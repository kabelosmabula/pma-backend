using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class UserPracticeConfiguration : IEntityTypeConfiguration<UserPractice>
    {
        public void Configure(EntityTypeBuilder<UserPractice> builder)
        {
            builder.ToTable("userpractices");

            builder.HasKey(up => new { up.UserId, up.PracticeId });

            builder.Property(d => d.createddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(o => o.isdeleted)
             .HasDefaultValue(false);

            builder.HasOne(up => up.User)
                   .WithMany(u => u.UserPractices)
                   .HasForeignKey(up => up.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(up => up.Practice)
                   .WithMany()
                   .HasForeignKey(up => up.PracticeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
