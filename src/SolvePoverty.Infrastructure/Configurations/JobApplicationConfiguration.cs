using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolvePoverty.Domain.Entities;

namespace SolvePoverty.Infrastructure.Configurations;

public class JobApplicationConfiguration : IEntityTypeConfiguration<JobApplication>
{
    public void Configure(EntityTypeBuilder<JobApplication> builder)
    {
        builder.HasOne(ja => ja.Job)
            .WithMany(j => j.Applications)
            .HasForeignKey(ja => ja.JobId)
            .IsRequired(false); // Make optional

        builder.HasOne(ja => ja.User)
            .WithMany(u => u.JobApplications)
            .HasForeignKey(ja => ja.UserId)
            .IsRequired(false); // Make optional

        builder.Property(ja => ja.CoverLetter)
            .IsRequired()
            .HasMaxLength(1000);
            
        builder.Property(ja => ja.ResumeUrl)
            .HasMaxLength(500);
    }
}