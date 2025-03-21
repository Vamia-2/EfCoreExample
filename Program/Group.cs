using System;
using System.Collections.Generic;

namespace EfCoreExample;

public partial class Group
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
