using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class ProcedureConfiguration : IEntityTypeConfiguration<Procedure>
    {
        public void Configure(EntityTypeBuilder<Procedure> builder)
        {
            builder.ToTable("procedures");

            builder.HasKey(p => p.id);

            builder.Property(p => p.code)
                .HasMaxLength(50);

            builder.Property(p => p.description)
                .HasMaxLength(500);

            builder.Property(p => p.name)
               .HasMaxLength(500);

            builder.Property(p => p.tariffAmount)
                .HasColumnType("numeric(18,2)");

            builder.Property(d => d.createddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(o => o.isdeleted)
             .HasDefaultValue(false);

            builder.HasOne(p => p.DoctorConsultation)
                .WithMany()
                .HasForeignKey(p => p.ConsultationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
