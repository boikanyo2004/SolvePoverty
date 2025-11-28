namespace SolvePoverty.Domain.Entities;

public class Donation : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public decimal Amount { get; set; }
    public string? Message { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string? TransactionId { get; set; }
    public DonationStatus Status { get; set; }
    public DateTime? ProcessedDate { get; set; }
}