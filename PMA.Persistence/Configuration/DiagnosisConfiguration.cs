using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class DiagnosisConfiguration : IEntityTypeConfiguration<Diagnosis>
    {
        public void Configure(EntityTypeBuilder<Diagnosis> builder)
        {
            builder.ToTable("diagnoses");

            builder.HasKey(d => d.id);

            builder.Property(cd => cd.ConsultationId)
               .IsRequired()
               .HasMaxLength(255);

            builder.Property(d => d.Code)
                .HasMaxLength(50);

            builder.Property(d => d.Description)
                .HasMaxLength(500);

            builder.Property(d => d.createddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(o => o.isdeleted)
             .HasDefaultValue(false);

            builder.HasOne(d => d.DoctorConsultation)
                .WithMany()
                .HasForeignKey(d => d.ConsultationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
