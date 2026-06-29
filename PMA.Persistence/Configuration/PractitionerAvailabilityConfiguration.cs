using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class PractitionerAvailabilityConfiguration : IEntityTypeConfiguration<PractitionerAvailability>
    {
        public void Configure(EntityTypeBuilder<PractitionerAvailability> builder)
        {
            builder.ToTable("practitioneravailabilities");

            builder.HasKey(a => a.id);

            builder.HasOne(a => a.PracticePractitioner)
                .WithMany(pp => pp.Availabilities)
                .HasForeignKey(a => a.PracticePractitionerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(d => d.createddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(o => o.isdeleted)
             .HasDefaultValue(false);

            builder.HasIndex(a => new
            {
                a.PracticePractitionerId,
                a.DayOfWeek,
                a.StartTime,
                a.EndTime
            }).IsUnique();
        }
    }
}
