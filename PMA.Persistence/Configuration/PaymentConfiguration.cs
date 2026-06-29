using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PMA.Domain.Entities
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("payments");
            builder.HasKey(p => p.id);

            builder.HasOne(x => x.Invoice)
               .WithMany(x => x.Payments)
               .HasForeignKey(x => x.InvoiceId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Property(d => d.createddate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
