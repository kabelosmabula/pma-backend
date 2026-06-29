using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class OncologyConsultationConfiguration : IEntityTypeConfiguration<OncologyConsultation>
    {
        public void Configure(EntityTypeBuilder<OncologyConsultation> builder)
        {
            builder.ToTable("OncologyConsultations");

            builder.HasKey(oc => oc.id);

            builder.Property(oc => oc.ConsultationId)
                .IsRequired();

            builder.Property(oc => oc.CancerType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(oc => oc.CancerStage)
                .HasMaxLength(20);

            builder.Property(oc => oc.ChemotherapyCycle)
                .HasDefaultValue(0);

            builder.Property(oc => oc.TumorResponse)
                .HasMaxLength(200);

        }
    }
}
