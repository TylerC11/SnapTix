namespace SnapTix.Models
{
    public class Owner
    {
        // Primary key
        public int OwnerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ContactInfo { get; set; } = string.Empty;
        // Navigation property
        public List<Sport>? Sports { get; set; }
    }
}
