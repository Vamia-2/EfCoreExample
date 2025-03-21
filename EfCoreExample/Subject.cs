using System;
using System.Collections.Generic;

namespace EfCoreExample;

public partial class Subject
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
