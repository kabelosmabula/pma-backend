using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using PMA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");

            builder.HasKey(a => a.id);

            builder.Property(a => a.Appointmentreference)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("AppointmentReference");

            builder.Property(a => a.Appointmenttype)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("AppointmentType");

            builder.Property(a => a.Priority)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.PracticeId)
                .IsRequired();

            builder.Property(a => a.PatientId)
                .IsRequired();

            builder.Property(a => a.IsFollowUp)
                .IsRequired();

            builder.Property(a => a.PractitionerId)
                .IsRequired()
                 .HasColumnName("PracticePractitionerId");

            builder.Property(a => a.HouseholdId)
                .IsRequired();

            builder.Property(a => a.AppointmentDate)
                .IsRequired();

            builder.Property(a => a.startappointment)
                .IsRequired()
                .HasColumnName("StartAppointment");

            builder.Property(a => a.endappointment)
                .IsRequired()
                .HasColumnName("EndAppointment");

            builder.Property(a => a.Status)
                .IsRequired()
                .HasConversion<int>()  
                .HasDefaultValue(AppointmentStatus.Booked); 

            builder.HasIndex(a => a.Appointmentreference)
                .IsUnique();  
            builder.HasIndex(a => new { a.PracticeId, a.AppointmentDate })
                .HasDatabaseName("IX_Appointments_Practice_Date");

            builder.HasIndex(a => a.PatientId);
            builder.HasIndex(a => a.PractitionerId);
            builder.HasIndex(a => a.HouseholdId);
            builder.HasIndex(a => a.Status);
            builder.HasIndex(a => a.AppointmentDate);

            builder.HasOne(a => a.Practice)
                .WithMany() 
                .HasForeignKey(a => a.PracticeId)
                .OnDelete(DeleteBehavior.Restrict);  

            builder.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)  
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Practitioner)
                .WithMany() 
                .HasForeignKey(a => a.PractitionerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.PatientHousehold)
                .WithMany() 
                .HasForeignKey(a => a.HouseholdId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
