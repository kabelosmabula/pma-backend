using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class ConsultationConfiguration : IEntityTypeConfiguration<Consultation>
    {
        public void Configure(EntityTypeBuilder<Consultation> builder)
        {
            builder.ToTable("consultation");

            builder.HasKey(v => v.id);

            builder.HasOne(v => v.ClinicalRecord)
                .WithMany(cr => cr.DoctorVisits)
                .HasForeignKey(v => v.ClinicalRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.PracticePractitioner)
                .WithMany()
                .HasForeignKey(v => v.PracticePractitionerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Invoice)
               .WithOne(x => x.Consultation)
               .HasForeignKey<Invoice>(x => x.ConsultationId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.Appointment)
                .WithOne()
                .HasForeignKey<Consultation>(v => v.AppointmentId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(v => v.VisitDate)
                .IsRequired();
            builder.Property(v => v.ParentConsultationId);

            builder.Property(v => v.SpecialtyTypes)
                .IsRequired();

            builder.Property(d => d.createddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(o => o.isdeleted)
             .HasDefaultValue(false);

            builder.HasOne(c => c.CardiologyConsultation)
            .WithOne(cc => cc.Consultation)
            .HasForeignKey<CardiologyConsultation>(cc => cc.ConsultationId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.DermatologyConsultation)
                .WithOne(dc => dc.Consultation)
                .HasForeignKey<DermatologyConsultation>(dc => dc.ConsultationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.PsychiatryConsultation)
                .WithOne(pc => pc.Consultation)
                .HasForeignKey<PsychiatryConsultation>(pc => pc.ConsultationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.PediatricsConsultation)
                .WithOne(pc => pc.Consultation)
                .HasForeignKey<PediatricsConsultation>(pc => pc.ConsultationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.OncologyConsultation)
                .WithOne(oc => oc.Consultation)
                .HasForeignKey<OncologyConsultation>(oc => oc.ConsultationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.DentistryConsultation)
                .WithOne(dc => dc.Consultation)
                .HasForeignKey<DentistryConsultation>(dc => dc.ConsultationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
