namespace SolvePoverty.Domain.Entities;

public class HelpOffer : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int? HelpRequestId { get; set; }
    public HelpRequest? HelpRequest { get; set; }
    public OfferType OfferType { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public OfferStatus Status { get; set; }
    public DateTime? AcceptedDate { get; set; }
}