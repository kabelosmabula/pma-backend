using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class PracticePractitionerConfiguration : IEntityTypeConfiguration<PracticePractitioner>
    {
        public void Configure(EntityTypeBuilder<PracticePractitioner> builder)
        {
            builder.ToTable("practicepractitioners");

            builder.HasKey(pp => pp.id);

            builder.HasIndex(pp => new { pp.PracticeId, pp.PractitionerId })
                .IsUnique();

            builder.Property(d => d.createddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(o => o.isdeleted)
             .HasDefaultValue(false);

            builder.HasOne(pp => pp.Practice)
                .WithMany(p => p.PracticePractitioners)
                .HasForeignKey(pp => pp.PracticeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pp => pp.Practitioner)
                .WithMany(p => p.PracticePractitioners)
                .HasForeignKey(pp => pp.PractitionerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
