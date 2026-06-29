using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class DermatologyConsultationConfiguration : IEntityTypeConfiguration<DermatologyConsultation>
    {
        public void Configure(EntityTypeBuilder<DermatologyConsultation> builder)
        {
            builder.ToTable("DermatologyConsultations");

            builder.HasKey(dc => dc.id);

            builder.Property(dc => dc.SkinCondition)
                .IsRequired();

            builder.Property(dc => dc.SkinCondition)
                .IsRequired();

            builder.Property(dc => dc.Severity)
                .HasMaxLength(50);

            builder.Property(dc => dc.AffectedAreas)
                .HasMaxLength(500);

            builder.HasMany(dc => dc.ClinicalImages)
                .WithOne(ci => ci.DermatologyConsultation)
                .HasForeignKey(ci => ci.DermatologyConsultationId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
