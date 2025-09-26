namespace SnapTix.Models
{
    public class Category
    {
        // Primary key
        public int CategoryId { get; set; }

        public string Name { get; set; } = string.Empty;

        // Navigation property
        public List<Sport>? Sports { get; set; }
    }
}
