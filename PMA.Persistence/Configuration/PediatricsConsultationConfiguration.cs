using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class PediatricsConsultationConfiguration : IEntityTypeConfiguration<PediatricsConsultation>
    {
        public void Configure(EntityTypeBuilder<PediatricsConsultation> builder)
        {
            builder.ToTable("PediatricsConsultations");

            builder.HasKey(pc => pc.id);

            builder.Property(pc => pc.ConsultationId)
                .IsRequired();

            builder.Property(pc => pc.WeightKg)
                .HasPrecision(5, 2);

            builder.Property(pc => pc.HeightCm)
                .HasPrecision(5, 2);

            builder.Property(pc => pc.VaccinationsUpToDate)
             .IsRequired();

            builder.Property(pc => pc.DevelopmentalMilestones)
                .HasMaxLength(1000);

        }
    }
}
