using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace SnapTix.Models
{
    public class Sport
    {
        // Primary key
        public int SportId { get; set; }

        // Foreign Keys
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Owner")]
        public int OwnerId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        [Display(Name = "Time of Event")]
        public DateTime SportDate { get; set; }

        // Photo upload
        [NotMapped]
        [Display(Name = "Photo")]
        public IFormFile? PhotoFile { get; set; }

        [Display(Name = "Photo Path")]
        public string? PhotoPath { get; set; }

        // Navigation properties
        [Display(Name = "Category")]
        public Category? Categorys { get; set; }

        [Display(Name = "Owner")]
        public Owner? Owners { get; set; }
    }
}
