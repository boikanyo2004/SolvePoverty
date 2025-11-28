using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolvePoverty.Domain.Entities;

namespace SolvePoverty.Infrastructure.Configurations;

public class HelpRequestConfiguration : IEntityTypeConfiguration<HelpRequest>
{
    public void Configure(EntityTypeBuilder<HelpRequest> builder)
    {
        builder.HasOne(hr => hr.User)
            .WithMany(u => u.HelpRequests)
            .HasForeignKey(hr => hr.UserId)
            .IsRequired(false); // Make optional

        builder.HasOne(hr => hr.FulfilledByUser)
            .WithMany()
            .HasForeignKey(hr => hr.FulfilledByUserId)
            .IsRequired(false); // Already optional due to nullable foreign key

        builder.Property(hr => hr.Title)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(hr => hr.Description)
            .IsRequired()
            .HasMaxLength(1000);
    }
}