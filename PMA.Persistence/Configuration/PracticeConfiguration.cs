using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class PracticeConfiguration : IEntityTypeConfiguration<Practice>
    {
        void IEntityTypeConfiguration<Practice>.Configure(EntityTypeBuilder<Practice> builder)
        {
            builder.ToTable("practices");

            builder.HasKey(p => p.id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(p => p.PracticeNumber)
                .IsUnique();

            builder.Property(d => d.createddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(o => o.isdeleted)
             .HasDefaultValue(false);

            builder.HasMany(p => p.PracticePractitioners)
            .WithOne(pr => pr.Practice)
            .HasForeignKey(pr => pr.PracticeId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
