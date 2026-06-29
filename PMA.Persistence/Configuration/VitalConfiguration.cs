using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class VitalConfiguration : IEntityTypeConfiguration<Vital>
    {
        public void Configure(EntityTypeBuilder<Vital> builder)
        {
            builder.ToTable("vitals");

            builder.HasKey(v => v.id);

            builder.Property(v => v.Temperature)
                .HasColumnType("numeric(5,2)");
   
            builder.Property(v => v.Weight)
                .HasColumnType("numeric(6,2)");

            builder.Property(v => v.Pulse);

            builder.Property(v => v.BMI);

            builder.Property(v => v.oxygenSaturation);

            builder.Property(v => v.respiratoryRate);

            builder.Property(v => v.SystolicBP);

            builder.Property(v => v.DiastolicBP);

            builder.Property(d => d.createddate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(o => o.isdeleted)
             .HasDefaultValue(false);

            builder.HasOne(v => v.consultation)
               .WithOne(c => c.Vital)
               .HasForeignKey<Vital>(v => v.consultationId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(v => v.consultationId)
                .IsUnique();
        }
    }
}
