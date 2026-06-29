using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class ClinicalImageConfiguration : IEntityTypeConfiguration<ClinicalImage>
    {
        public void Configure(EntityTypeBuilder<ClinicalImage> builder)
        {
            builder.ToTable("ClinicalImages");

            builder.HasKey(ci => ci.id);

            builder.Property(ci => ci.DermatologyConsultationId)
                .IsRequired();

            builder.Property(ci => ci.ImageUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(ci => ci.Description)
                .HasMaxLength(500);

            builder.Property(ci => ci.CapturedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");


        }
    }
}
