namespace SnapTix.Models
{
    public class Ticket
    {
        // Primary key
        public int TicketId { get; set; }

        // Foreign key
        public int EventId { get; set; }

        // The Ticket's filename on the server
        public string Owner { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }

        // Navigation property (code)
        public Event? Events { get; set; }

    }
}
