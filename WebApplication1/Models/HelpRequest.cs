using System;

namespace WebApplication1.Models
{
    public class HelpRequest
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty; // Food, Clothing, Shelter, etc.
        public string Location { get; set; } = string.Empty;
        public bool IsUrgent { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string ContactEmail { get; set; } = string.Empty;
        public string Status { get; set; } = "Open"; // Open, InProgress, Fulfilled
    }
}