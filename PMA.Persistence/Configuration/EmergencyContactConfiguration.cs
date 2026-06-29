using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class EmergencyContactConfiguration : IEntityTypeConfiguration<EmergencyContact>
    {
        public void Configure(EntityTypeBuilder<EmergencyContact> builder)
        {
            builder.ToTable("emergancycontact");

            builder.HasKey(a => a.id);

            builder.Property(a => a.phone)
                .IsRequired()
                .HasMaxLength(50);


            builder.Property(a => a.relationship)
                .HasMaxLength(100);

            builder.Property(a => a.name)
                .HasMaxLength(50);

            builder.Property(d => d.createddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(o => o.isdeleted)
             .HasDefaultValue(false);

            builder.HasOne(a => a.Patient)
               .WithMany(p => p.EmergencyContacts)
               .HasForeignKey(a => a.PatientId)
               .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
