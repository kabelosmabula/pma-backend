using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class CardiologyConsultationConfiguration : IEntityTypeConfiguration<CardiologyConsultation>
    {
        public void Configure(EntityTypeBuilder<CardiologyConsultation> builder)
        {
            builder.ToTable("CardiologyConsultation");

            builder.HasKey(cc => cc.id);

            builder.Property(pc => pc.ConsultationId)
               .IsRequired();

            builder.Property(cc => cc.ECGFindings)
                .HasMaxLength(500);

            builder.Property(cc => cc.EjectionFraction)
                .HasMaxLength(100);

            builder.Property(cc => cc.ECGPerformed)
                .HasMaxLength(500);
            builder.Property(cc => cc.EchoPerformed)
               .HasMaxLength(500);
            builder.Property(cc => cc.SmokingRisk)
               .HasMaxLength(500);
            builder.Property(cc => cc.HypertensionRisk)
               .HasMaxLength(500);
        }
    }
}
