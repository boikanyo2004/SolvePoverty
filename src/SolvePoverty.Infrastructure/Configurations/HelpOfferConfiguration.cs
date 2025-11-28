using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolvePoverty.Domain.Entities;

namespace SolvePoverty.Infrastructure.Configurations;

public class HelpOfferConfiguration : IEntityTypeConfiguration<HelpOffer>
{
    public void Configure(EntityTypeBuilder<HelpOffer> builder)
    {
        builder.HasOne(ho => ho.User)
            .WithMany(u => u.HelpOffers)
            .HasForeignKey(ho => ho.UserId)
            .IsRequired(false); // Make optional

        builder.HasOne(ho => ho.HelpRequest)
            .WithMany(hr => hr.HelpOffers)
            .HasForeignKey(ho => ho.HelpRequestId)
            .IsRequired(false); // Already optional due to nullable foreign key

        builder.Property(ho => ho.Title)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(ho => ho.Description)
            .IsRequired()
            .HasMaxLength(1000);
    }
}