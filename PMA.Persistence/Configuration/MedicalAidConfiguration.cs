using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class MedicalAidConfiguration : IEntityTypeConfiguration<MedicalAid>
    {
        public void Configure(EntityTypeBuilder<MedicalAid> builder)
        {
            builder.ToTable("medicalAid");

            builder.HasKey(pma => pma.id);

            builder.Property(pma => pma.SchemeCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(pma => pma.MembershipNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(d => d.createddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(o => o.isdeleted)
             .HasDefaultValue(false);

            builder.HasOne(pma => pma.Patient)
                .WithMany(p => p.MedicalAids)
                .HasForeignKey(pma => pma.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(pma => new { pma.PatientId, pma.IsActive })
                .IsUnique()
                .HasFilter("[IsActive] = 1");
        }
    }
}
