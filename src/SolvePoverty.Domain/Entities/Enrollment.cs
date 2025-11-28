namespace SolvePoverty.Domain.Entities;

public class Enrollment : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public int ProgressPercentage { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedDate { get; set; }
}