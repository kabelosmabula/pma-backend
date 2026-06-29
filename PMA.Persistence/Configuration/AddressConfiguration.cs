using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("addresses");

            builder.HasKey(a => a.id);

            builder.Property(a => a.streetAddress)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.city)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.province)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(a => a.postalCode)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(d => d.createddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(o => o.isdeleted)
                .HasDefaultValue(false);

            builder.HasOne(a => a.Patient)
                .WithMany(p => p.Addresses)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
