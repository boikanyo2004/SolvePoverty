namespace SolvePoverty.Domain.Entities;

public class Message : BaseEntity
{
    public int SenderId { get; set; }
    public User Sender { get; set; } = null!;
    public int ReceiverId { get; set; }
    public User Receiver { get; set; } = null!;
    public string Content { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime? ReadDate { get; set; }
}