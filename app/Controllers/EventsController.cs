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

        // SQL que je ferais:
        // SELECT * FROM events LEFT JOIN locations ON events.location_id = locations.id ORDER BY events.start ASC
        //
        // SQL réel :
        // SELECT [e].[id], [e].[category_id], [e].[description], [e].[end], [e].[location_id], [e].[name], [e].[start], [l].[id], [l].[address], [l].[name], [c].[id], [c].[name]
        // FROM[events] AS[e]
        // INNER JOIN [locations] AS [l] ON [e].[location_id] = [l].[id]
        // INNER JOIN [categories] AS [c] ON [e].[category_id] = [c].[id]
        // ORDER BY[e].[start]
        //
        // .Select() gère le mappage vers des objets EventViewModel.
        // .ToListAsync() liste les EventViewModel dans une List<EventViewModel> de façon asynchrone.
        public async Task<IActionResult> Index()
        {
            var eventViewModels = await _context.Events // SELECT * FROM events
                .Include(e => e.Location) // LEFT JOIN locations ON events.location_id = locations.id
                .Include(e => e.Category) // LEFT JOIN categories ON events.category_id = categories.id
                .OrderBy(e => e.Start) // ORDER BY events.start ASC
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
