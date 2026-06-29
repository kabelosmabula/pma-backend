using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class ClinicalDocumentConfiguration : IEntityTypeConfiguration<ClinicalDocument>
    {
        public void Configure(EntityTypeBuilder<ClinicalDocument> builder)
        {
            builder.ToTable("clinicaldocuments");

            builder.HasKey(cd => cd.id);

            builder.Property(cd => cd.documentName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(cd => cd.documentType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(cd => cd.fileUrl)
                .IsRequired();

            builder.Property(d => d.createddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(o => o.isdeleted)
             .HasDefaultValue(false);

            builder.HasOne(cd => cd.DoctorConsultation)
                .WithMany()
                .HasForeignKey(cd => cd.ConsultationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
