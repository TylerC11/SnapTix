using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SnapTix.Data;
using SnapTix.Models;

namespace SnapTix.Controllers
{
    public class SportsController : Controller
    {
        private readonly SnapTixContext _context;

        public SportsController(SnapTixContext context)
        {
            _context = context;
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
                    string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);

                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(sport.PhotoFile.FileName);
                    string filePath = Path.Combine(uploadDir, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await sport.PhotoFile.CopyToAsync(fileStream);
                    }

                    // Save path for use in <img src="">
                    sport.PhotoPath = "/images/" + uniqueFileName;
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
                    // Handle new photo upload
                    if (sport.PhotoFile != null && sport.PhotoFile.Length > 0)
                    {
                        string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                        if (!Directory.Exists(uploadDir))
                            Directory.CreateDirectory(uploadDir);

                        string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(sport.PhotoFile.FileName);
                        string filePath = Path.Combine(uploadDir, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await sport.PhotoFile.CopyToAsync(fileStream);
                        }

                        sport.PhotoPath = "/images/" + uniqueFileName;
                    }
                    else
                    {
                        // Preserve existing photo if no new file is uploaded
                        var existingSport = await _context.Sport.AsNoTracking()
                            .FirstOrDefaultAsync(s => s.SportId == sport.SportId);

                        if (existingSport != null)
                            sport.PhotoPath = existingSport.PhotoPath;
                    }

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
