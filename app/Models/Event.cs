using System;
using System.Collections.Generic;

namespace app.Models;

public partial class Event
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public int LocationId { get; set; }

    public virtual Location Location { get; set; } = null!;
}
