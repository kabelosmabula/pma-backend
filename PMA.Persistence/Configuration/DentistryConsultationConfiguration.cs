using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class DentistryConsultationConfiguration : IEntityTypeConfiguration<DentistryConsultation>
    {
        public void Configure(EntityTypeBuilder<DentistryConsultation> builder)
        {
            builder.ToTable("DentistryConsultations");

            builder.HasKey(dc => dc.id);

            builder.Property(dc => dc.ConsultationId)
                .IsRequired();

            builder.Property(dc => dc.XRayCompleted)
                .IsRequired();

            builder.Property(dc => dc.GumCondition)
                .HasMaxLength(200);

            builder.HasMany(dc => dc.ToothCharts)
                .WithOne(tc => tc.DentistryConsultation)
                .HasForeignKey(tc => tc.DentistryConsultationId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
