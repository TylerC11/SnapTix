namespace SnapTix.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Title { get; set; } = string.Empty;

        // Foreign key to Ticket
        public int PhotoId { get; set; }

        // Navigation property (code)
        public Ticket? Ticket { get; set; }
    }
}
