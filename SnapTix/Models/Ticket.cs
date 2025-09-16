namespace SnapTix.Models
{
    public class Ticket
    {
        // Primary key
        public int TicketId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // The Ticket's filename on the server
        public string Owner { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }

        // Navigation property (code)
        public List<Tag>? Tags { get; set; }
    }
}
