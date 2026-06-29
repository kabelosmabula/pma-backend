using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class ClinicalRecordConfiguration : IEntityTypeConfiguration<ClinicalRecord>
    {
        public void Configure(EntityTypeBuilder<ClinicalRecord> builder)
        {
            builder.ToTable("clinicalrecords");

            builder.HasKey(cr => cr.id);

            builder.HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey(x => x.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Practice)
                .WithMany()
                .HasForeignKey(x => x.PracticeId)
                .OnDelete(DeleteBehavior.Restrict);
            //builder.HasOne(cr => cr.Practice)
            //    .WithMany(p => p.ClinicalRecords) 
            //    .HasForeignKey(cr => cr.PracticeId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(cr => cr.Patient)
            //    .WithMany(p => p.ClinicalRecords)
            //    .HasForeignKey(cr => cr.PatientId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(cr => cr.DoctorVisits)
                .WithOne(c => c.ClinicalRecord)
                .HasForeignKey(c => c.ClinicalRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(cr => new { cr.PatientId, cr.PracticeId })
                .IsUnique();

            builder.Property(d => d.createddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(o => o.isdeleted)
             .HasDefaultValue(false);
        }
    }
}
