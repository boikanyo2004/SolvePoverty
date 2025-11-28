namespace SolvePoverty.Domain.Entities;

public class HelpRequest : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public RequestType RequestType { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsUrgent { get; set; }
    public RequestStatus Status { get; set; }
    public DateTime? FulfilledDate { get; set; }
    public int? FulfilledByUserId { get; set; }
    public User? FulfilledByUser { get; set; }

    public ICollection<HelpOffer> HelpOffers { get; set; } = new List<HelpOffer>();
}