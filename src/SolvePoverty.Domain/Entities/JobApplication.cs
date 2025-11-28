namespace SolvePoverty.Domain.Entities;

public class JobApplication : BaseEntity
{
    public int JobId { get; set; }
    public Job Job { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public string CoverLetter { get; set; } = string.Empty;
    public string? ResumeUrl { get; set; }
    public ApplicationStatus Status { get; set; }
    public DateTime? ReviewedDate { get; set; }
}