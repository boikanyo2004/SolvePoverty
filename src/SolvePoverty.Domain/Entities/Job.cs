namespace SolvePoverty.Domain.Entities;

public class Job : BaseEntity
{
    public int PostedByUserId { get; set; }
    public User PostedByUser { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public JobType JobType { get; set; }
    public decimal? SalaryMin { get; set; }
    public decimal? SalaryMax { get; set; }
    public string Requirements { get; set; } = string.Empty;
    public JobStatus Status { get; set; }
    public DateTime? ClosedDate { get; set; }

    public ICollection<JobApplication> Applications { get; set; } = new List<JobApplication>();
}