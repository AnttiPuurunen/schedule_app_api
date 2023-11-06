using System;
using System.Collections.Generic;

namespace ScheduleAppApi.Models;

public partial class Tasktype
{
    public int TaskTypeId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
