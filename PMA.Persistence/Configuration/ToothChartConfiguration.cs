using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class ToothChartConfiguration : IEntityTypeConfiguration<ToothChart>
    {
        public void Configure(EntityTypeBuilder<ToothChart> builder)
        {
            builder.ToTable("ToothCharts");

            builder.HasKey(tc => tc.id);
            
             builder.Property(tc => tc.DentistryConsultationId)
                .IsRequired();

            builder.Property(tc => tc.ToothNumber)
                .IsRequired();

            builder.Property(tc => tc.Condition)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(tc => tc.Notes)
                .HasMaxLength(500);

        }
    }
}
