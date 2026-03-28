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

            if (events == null)
            {
                return NotFound();
            }
            else
            {
                return View(events);
            }
        }

        public IActionResult Details(int id)
        {
            var evento = _context.Events.Find(id);

            if (evento == null)
            {
                return NotFound();
            }
            else
            {
                return View(evento);
            }
        }
    }
}
