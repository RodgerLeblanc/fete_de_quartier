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

        // SQL: SELECT * FROM fete_de_quartier.dbo.events ORDER BY start ASC LEFT JOIN fete_de_quartier.dbo.locations
        // .Select() gère le mappage vers des objets EventViewModel.
        // .ToListAsync() liste les EventViewModel dans une List<EventViewModel> de façon asynchrone.
        public async Task<IActionResult> Index()
        {
            var eventViewModels = await _context.Events // SELECT * FROM fete_de_quartier.dbo.events
                .OrderBy(e => e.Start) // ORDER BY start ASC
                .Include(e => e.Location) // LEFT JOIN fete_de_quartier.dbo.locations
                .Select(e => new EventViewModel(e)) // Mape vers des objets EventViewModel.
                .ToListAsync(); // Liste les EventViewModel dans une List<EventViewModel> de façon asynchrone.

            if (eventViewModels == null)
            {
                return NotFound();
            }
            else
            {
                return View(eventViewModels);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            // Pas de requête SQL, puisque données déjà chargées par la tâche Index().
            var evento = await _context.Events.FindAsync(id);

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
