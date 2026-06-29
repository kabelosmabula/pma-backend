using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class PsychiatryConsultationConfiguration : IEntityTypeConfiguration<PsychiatryConsultation>
    {
        public void Configure(EntityTypeBuilder<PsychiatryConsultation> builder)
        {
            builder.ToTable("PsychiatryConsultations");

            builder.HasKey(pc => pc.id);

            builder.Property(pc => pc.ConsultationId)
                .IsRequired();

            builder.Property(pc => pc.Mood)
                .HasMaxLength(100);

            builder.Property(pc => pc.Affect)
                .HasMaxLength(100);

            builder.Property(pc => pc.Speech)
                .HasMaxLength(100);

            builder.Property(pc => pc.SelfHarmRiskLevel)
                .HasMaxLength(50);

            builder.Property(pc => pc.SuicidalIdeation)
               .IsRequired();

            builder.Property(pc => pc.TherapyNotes)
                .HasMaxLength(2000);

        }
    }
}
