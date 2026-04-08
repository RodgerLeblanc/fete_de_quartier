namespace app.Models
{
    public class EventViewModel
    {
        // Membres de la classe 'Event'
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        // Membre de la classe 'Category' aplati
        public string CategoryName { get; set; } = null!;

        // Membres de la classe 'Location' aplatis
        public string LocationName { get; set; } = null!;

        public string? LocationAddress { get; set; }

        public EventViewModel(Event e)
        {
            Id = e.Id;
            Name = e.Name;
            Description = e.Description;
            Start = e.Start;
            End = e.End;
            CategoryName = e.Category.Name;
            LocationName = e.Location.Name;
            LocationAddress = e.Location.Address;
        }
    }
}
