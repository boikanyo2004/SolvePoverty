namespace SolvePoverty.Domain.Entities;

public class Course : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public int EstimatedHours { get; set; }
    public string Category { get; set; } = string.Empty;
    public string? ThumbnailUrl { get; set; }
    public string? VideoUrl { get; set; }
    public string Content { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
    public int EnrollmentCount { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public ICollection<CourseModule> Modules { get; set; } = new List<CourseModule>();
}