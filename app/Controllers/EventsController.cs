using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using app.Models;
using app.Data;

namespace app.Controllers
{
    public class EventsController : Controller
    {
        private readonly FeteDeQuartierContext _context;

        public EventsController(FeteDeQuartierContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _context.Events.OrderBy(e => e.Start).Include(e => e.Location).ToListAsync();

            return View(events);
        }
    }
}
