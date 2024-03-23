using System;
using System.Collections.Generic;

namespace TodoList.Models;

public partial class Task
{
    public uint Id { get; set; }

    public uint CategoryId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Updated { get; set; }

    public virtual Category Category { get; set; } = null!;
}
