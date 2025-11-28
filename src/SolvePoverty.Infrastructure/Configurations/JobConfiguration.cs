using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolvePoverty.Domain.Entities;

namespace SolvePoverty.Infrastructure.Configurations;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.HasOne(j => j.PostedByUser)
            .WithMany()
            .HasForeignKey(j => j.PostedByUserId)
            .IsRequired(false); // Make optional

        builder.Property(j => j.Title)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(j => j.Company)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(j => j.Location)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(j => j.Requirements)
            .IsRequired()
            .HasMaxLength(1000);
            
        builder.Property(j => j.Description)
            .IsRequired()
            .HasMaxLength(2000);
            
        builder.Property(j => j.SalaryMin)
            .HasPrecision(18, 2);
            
        builder.Property(j => j.SalaryMax)
            .HasPrecision(18, 2);
    }
}