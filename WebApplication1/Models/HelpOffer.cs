namespace WebApplication1.Models
{
    public class HelpOffer
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string OfferType { get; set; } = string.Empty; // Job, Donation, Service, etc.
    }
}