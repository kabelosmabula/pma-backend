using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PMA.Domain.Entities;
using PMA.Persistence.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PMA.Persistence
{
    public class PMADBContext :DbContext
    {
        public PMADBContext(DbContextOptions<PMADBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }  
        public DbSet<Practice> Practice { get; set; }
        public DbSet<UserPractice> UserPractices { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }  
        public DbSet<Role> Roles { get; set; }
        public DbSet<MedicalAid> MedicalAids { get; set; }  
        public DbSet<Appointment> Appointments { get; set; }    
        public DbSet<ClinicalDocument> ClinicalDocuments { get; set; }  
        public DbSet<ClinicalRecord> ClinicalRecords { get; set; }
        public DbSet<Consultation> Consultations { get; set; }  
        public DbSet<Diagnosis> Diagnoses { get; set; } 
        public DbSet<Invoice> Invoices { get; set; }    
        public DbSet<PatientHousehold> PatientHousehold { get; set; }    
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Prescription> Prescriptions    { get; set; }
        public DbSet<Vital> Vitals { get; set; }
        public DbSet<InvoiceLineItem> InvoiceLineItems { get; set; }  
        public DbSet<Practitioner>  Practitioners { get; set; } 
        public DbSet<PracticePractitioner> PracticePractitioners { get; set; }  
        public DbSet<PractitionerAvailability> PractitionerAvailabilities { get; set; }
        public DbSet<Organisation> Organisations { get; set; }
        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<CardiologyConsultation> CardiologyConsultations { get; set; }
        public DbSet<DermatologyConsultation> DermatologyConsultations { get; set; }
        public DbSet<PsychiatryConsultation> PsychiatryConsultations { get; set; }
        public DbSet<PediatricsConsultation> PediatricsConsultations { get; set; }
        public DbSet<OncologyConsultation> OncologyConsultations { get; set; }
        public DbSet<DentistryConsultation> DentistryConsultations { get; set; }
        public DbSet<ClinicalImage> ClinicalImages { get; set; }
        public DbSet<ToothChart> ToothCharts { get; set; }


        //public DbSet<PractitionerAvailabilityException> PractitionerAvailabilityExceptions { get; set; }   


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PracticeConfiguration());
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(PMADBContext)
                        .GetMethod(nameof(SetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)
                        ?.MakeGenericMethod(entityType.ClrType);

                    method?.Invoke(null, new object[] { modelBuilder });
                }
            }
        }
        private static void SetSoftDeleteFilter<T>(ModelBuilder modelBuilder) where T : BaseEntity
        {
            modelBuilder.Entity<T>().HasQueryFilter(e => !e.isdeleted);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity.id == Guid.Empty)
                            entry.Entity.id = Guid.NewGuid();

                        entry.Entity.createddate = DateTime.UtcNow;
                        entry.Entity.isdeleted = false;
                        break;

                    case EntityState.Modified:
                        entry.Entity.modifieddate = DateTime.UtcNow;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.isdeleted = true;
                        entry.Entity.deleteddate = DateTime.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
