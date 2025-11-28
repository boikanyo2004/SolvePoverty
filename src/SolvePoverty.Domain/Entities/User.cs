namespace SolvePoverty.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserType UserType { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public bool IsVerified { get; set; }
    public DateTime? LastLoginDate { get; set; }

    public ICollection<HelpRequest> HelpRequests { get; set; } = new List<HelpRequest>();
    public ICollection<HelpOffer> HelpOffers { get; set; } = new List<HelpOffer>();
    public ICollection<JobApplication> JobApplications { get; set; } = new List<JobApplication>();
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public ICollection<Donation> Donations { get; set; } = new List<Donation>();
    public ICollection<Message> SentMessages { get; set; } = new List<Message>();
    public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
}