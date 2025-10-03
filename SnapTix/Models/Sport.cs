using System.ComponentModel.DataAnnotations;

namespace SnapTix.Models
{
    public class Sport
    {
        // Primary key
        public int SportId { get; set; }
        //Foreign Key
        public int CategoryId { get; set; }
        public int OwnerId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        [Display(Name = "Time of Event")]
        public DateTime SportDate { get; set; }

        // Navigation property (code)
        [Display(Name = "Category")]
        public Category? Categorys { get; set; }
        [Display(Name = "Owner")]
        public Owner? Owners { get; set; }
       
    }
}
