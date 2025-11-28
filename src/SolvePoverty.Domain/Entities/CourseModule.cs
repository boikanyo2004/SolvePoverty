namespace SolvePoverty.Domain.Entities;

public class CourseModule : BaseEntity
{
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int OrderIndex { get; set; }
    public int DurationMinutes { get; set; }
}