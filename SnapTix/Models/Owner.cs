namespace SnapTix.Models
{
    public class Owner
    {
        // Primary key
        public int OwnerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        // Navigation property
        public List<Event>? Events { get; set; }
    }
}
