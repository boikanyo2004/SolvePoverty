using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolvePoverty.Domain.Entities;

namespace SolvePoverty.Infrastructure.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasOne(m => m.Sender)
            .WithMany(u => u.SentMessages)
            .HasForeignKey(m => m.SenderId)
            .IsRequired(false) // Make optional
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Receiver)
            .WithMany(u => u.ReceivedMessages)
            .HasForeignKey(m => m.ReceiverId)
            .IsRequired(false) // Make optional
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(m => m.Content)
            .IsRequired()
            .HasMaxLength(2000);
    }
}