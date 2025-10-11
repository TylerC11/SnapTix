using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SnapTix.Models;

namespace SnapTix.Pages.Sports
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Sport Sport { get; set; }

        public SelectList CategoryId { get; set; }
        public SelectList OwnerId { get; set; }

        public void OnGet()
        {
            ViewData["Title"] = "Edit";
            // Example data, replace with your data source
            CategoryId = new SelectList(new[] { new { Id = 1, Name = "Soccer" }, new { Id = 2, Name = "Basketball" } }, "Id", "Name");
            OwnerId = new SelectList(new[] { new { Id = 1, Name = "Alice" }, new { Id = 2, Name = "Bob" } }, "Id", "Name");
            Sport = new Sport(); // Or load from your data source
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Re-populate lists if validation fails
                OnGet();
                return Page();
            }
            // Save logic here
            return RedirectToPage("/Index");
        }
    }
}