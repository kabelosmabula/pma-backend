using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.ToTable("prescriptions");

            builder.HasKey(p => p.id);

            builder.Property(p => p.medicationName).IsRequired().HasMaxLength(200);
            builder.Property(p => p.frequency).IsRequired().HasMaxLength(200);

            builder.Property(p => p.notes).IsRequired().HasMaxLength(200);
            builder.Property(p => p.duration).IsRequired().HasMaxLength(200);
            builder.Property(p => p.dosage).HasMaxLength(200);
            builder.Property(d => d.createddate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(o => o.isdeleted).HasDefaultValue(false);
            builder.Property(p => p.StartDate).HasColumnType("timestamp with time zone");
            builder.HasOne(p => p.consultation).WithMany().HasForeignKey(p => p.consultationId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
