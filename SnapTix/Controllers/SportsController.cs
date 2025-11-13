using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SnapTix.Data;
using SnapTix.Models;

namespace SnapTix.Controllers
{
    [Authorize] //restricted access
    public class SportsController : Controller
    {
        private readonly SnapTixContext _context;
        private readonly IConfiguration _configuration;
        private readonly BlobContainerClient _containerClient;

        public SportsController(SnapTixContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

            var connectionString = _configuration["AzureStorage"];
            var containerName = "snaptix-uploads";
            _containerClient = new BlobContainerClient(connectionString, containerName);
        }

        // GET: Sports
        public async Task<IActionResult> Index()
        {
            var snapTixContext = _context.Sport.Include(s => s.Categorys).Include(s => s.Owners);
            return View(await snapTixContext.ToListAsync());
        }

        // GET: Sports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var sport = await _context.Sport
                .Include(s => s.Categorys)
                .Include(s => s.Owners)
                .Include(s => s.Purchases)
                .FirstOrDefaultAsync(m => m.SportId == id);

            if (sport == null) return NotFound();

            return View(sport);
        }

        // GET: Sports/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name");
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "Name");
            return View();
        }

        // POST: Sports/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SportId,CategoryId,OwnerId,Title,Description,Location,SportDate,PhotoFile")] Sport sport)
        {
            if (ModelState.IsValid)
            {
                if (sport.PhotoFile != null && sport.PhotoFile.Length > 0)
                {
                    var fileUpload = sport.PhotoFile;

                    // the name of the file to save in Azure
                    string blobName = Guid.NewGuid().ToString() + fileUpload.FileName;

                    var blobClient = _containerClient.GetBlobClient(blobName);

                    using (var stream = fileUpload.OpenReadStream())
                    {
                        await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = fileUpload.ContentType });
                    }
                    //Get URL of the uploaded/blob file
                    string fileURL = blobClient.Uri.ToString();
                    sport.PhotoPath = fileURL;
                }

                _context.Add(sport);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

                ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", sport.CategoryId);
                ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "Name", sport.OwnerId);

                return View(sport);
            }
        // GET: Sports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var sport = await _context.Sport.FindAsync(id);
            if (sport == null) return NotFound();

            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", sport.CategoryId);
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "Name", sport.OwnerId);
            return View(sport);
        }

        // POST: Sports/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SportId,CategoryId,OwnerId,Title,Description,Location,SportDate,PhotoPath,PhotoFile")] Sport sport)
        {
            if (id != sport.SportId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Get the existing record for comparison
                    var existingSport = await _context.Sport.AsNoTracking()
                        .FirstOrDefaultAsync(s => s.SportId == sport.SportId);

                    if (existingSport == null)
                        return NotFound();

                    if (sport.PhotoFile != null && sport.PhotoFile.Length > 0)
                    {
                        var fileUpload = sport.PhotoFile;

                        // Give the blob a unique name
                        string blobName = Guid.NewGuid().ToString() + fileUpload.FileName;

                        var blobClient = _containerClient.GetBlobClient(blobName);

                        using (var stream = fileUpload.OpenReadStream())
                        {
                            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = fileUpload.ContentType });
                        }

                        // Save the blob URL to PhotoPath
                        sport.PhotoPath = blobClient.Uri.ToString();
                    }
                    else
                    {
                        // Keep existing photo if user didn’t upload a new one
                        sport.PhotoPath = existingSport.PhotoPath;
                    }

                    // Update the database record
                    _context.Update(sport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Sport.Any(e => e.SportId == sport.SportId))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction("Index", "Home");
            }

            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "CategoryId", "Name", sport.CategoryId);
            ViewData["OwnerId"] = new SelectList(_context.Set<Owner>(), "OwnerId", "Name", sport.OwnerId);
            return View(sport);
        }

        // GET: Sports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var sport = await _context.Sport
                .Include(s => s.Categorys)
                .Include(s => s.Owners)
                .Include(s => s.Purchases)
                .FirstOrDefaultAsync(m => m.SportId == id);

            if (sport == null) return NotFound();

            return View(sport);
        }

        // POST: Sports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sport = await _context.Sport.FindAsync(id);
            if (sport != null)
                _context.Sport.Remove(sport);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool SportExists(int id)
        {
            return _context.Sport.Any(e => e.SportId == id);
        }
    }
}
