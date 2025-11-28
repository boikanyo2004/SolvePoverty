using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolvePoverty.Domain.Entities;

namespace SolvePoverty.Infrastructure.Configurations;

public class DonationConfiguration : IEntityTypeConfiguration<Donation>
{
    public void Configure(EntityTypeBuilder<Donation> builder)
    {
        builder.HasOne(d => d.User)
            .WithMany(u => u.Donations)
            .HasForeignKey(d => d.UserId)
            .IsRequired(false); // Make optional

        builder.Property(d => d.Amount)
            .HasPrecision(18, 2);
            
        builder.Property(d => d.PaymentMethod)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(d => d.TransactionId)
            .HasMaxLength(100);
            
        builder.Property(d => d.Message)
            .HasMaxLength(500);
    }
}