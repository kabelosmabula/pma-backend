using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");

            builder.HasKey(r => r.id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.Description)
                .HasMaxLength(500);

            builder.HasIndex(r => r.Name)
                .IsUnique();

            builder.Property(d => d.createddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(o => o.isdeleted)
             .HasDefaultValue(false);
        }
    }
}
