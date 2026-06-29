using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;


namespace PMA.Persistence.Configuration
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("patients");

            builder.HasKey(p => p.id);

            builder.HasIndex(x => new { x.PracticeId, x.Identitynumber })
                .HasFilter("\"isdeleted\" = false")
                .IsUnique();

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.createddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(o => o.isdeleted)
             .HasDefaultValue(false);

            builder.HasOne(p => p.Practice)
                .WithMany(pr => pr.Patients)
                .HasForeignKey(p => p.PracticeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.ClinicalRecord)
                .WithOne(cr => cr.Patient)
                .HasForeignKey<ClinicalRecord>(cr => cr.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
