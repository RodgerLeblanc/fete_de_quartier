namespace app.Models
{
    public class Event
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required DateTime Start { get; set; }
        public required DateTime End { get; set; }
        public required string Location { get; set; }
    }
}