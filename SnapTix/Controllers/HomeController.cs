using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnapTix.Data;
using System.Diagnostics;

namespace SnapTix.Controllers
{
    [Authorize] //restricted access
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SnapTixContext _context;

        public HomeController(ILogger<HomeController> logger, SnapTixContext context)
        {
            _logger = logger;
            _context = context;
        }
            public async Task<IActionResult> Index()
        {
            var snapTixContext = _context.Sport.Include(s => s.Categorys).Include(s => s.Owners);
            return View(await snapTixContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sport = await _context.Sport
                .Include(s => s.Categorys)
                .Include(s => s.Owners)
                .Include(s => s.Purchases)
                .FirstOrDefaultAsync(m => m.SportId == id);
            if (sport == null)
            {
                return NotFound();
            }

            return View(sport);
        }


    }
}
