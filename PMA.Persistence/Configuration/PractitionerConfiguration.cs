using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class PractitionerConfiguration : IEntityTypeConfiguration<Practitioner>
    {
        public void Configure(EntityTypeBuilder<Practitioner> builder)
        {
            builder.ToTable("practitioners");

            builder.HasKey(p => p.id);

            builder.Property(p => p.HPCSANumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(p => p.HPCSANumber)
                .IsUnique();

            builder.Property(d => d.createddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(o => o.isdeleted)
             .HasDefaultValue(false);

            builder.HasMany(p => p.PracticePractitioners)
           .WithOne(pr => pr.Practitioner)
           .HasForeignKey(pr => pr.PractitionerId)
           .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.User)
              .WithOne()
              .HasForeignKey<Practitioner>(p => p.UserId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
