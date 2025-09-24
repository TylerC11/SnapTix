namespace SnapTix.Models
{
    public class Event
    {
        // Primary key
        public int EventId { get; set; }

        // Foreign keys
        public int CategoryId { get; set; }
        public int OwnerId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }

        // Navigation property (code)
        public Category? Categorys { get; set; }
        public Owner? Owners { get; set; }
    }
}
