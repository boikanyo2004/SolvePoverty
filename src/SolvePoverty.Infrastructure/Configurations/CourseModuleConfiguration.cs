using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolvePoverty.Domain.Entities;

namespace SolvePoverty.Infrastructure.Configurations;

public class CourseModuleConfiguration : IEntityTypeConfiguration<CourseModule>
{
    public void Configure(EntityTypeBuilder<CourseModule> builder)
    {
        builder.HasOne(cm => cm.Course)
            .WithMany(c => c.Modules)
            .HasForeignKey(cm => cm.CourseId)
            .IsRequired(false); // Make optional

        builder.Property(cm => cm.Title)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(cm => cm.Content)
            .IsRequired();
    }
}