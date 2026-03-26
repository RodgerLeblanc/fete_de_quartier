using System;
using System.Collections.Generic;

namespace app.Models;

public partial class Location
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
