using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Persistence.Configuration
{
    public class InvoiceLineItemConfiguration : IEntityTypeConfiguration<InvoiceLineItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceLineItem> builder)
        {
            builder.ToTable("invoicelineitems");

            builder.HasKey(li => li.id);

            builder.Property(li => li.InvoiceId)
                .HasMaxLength(100);

            builder.Property(li => li.ReferenceCode)
               .HasMaxLength(100);

            builder.Property(li => li.Description)
                .HasMaxLength(500);

            builder.Property(li => li.Amount)
                .HasColumnType("numeric(18,2)");

            builder.Property(d => d.createddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(o => o.isdeleted)
             .HasDefaultValue(false);

            builder.HasOne(li => li.Invoice)
                .WithMany(i => i.LineItems)
                .HasForeignKey(li => li.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
