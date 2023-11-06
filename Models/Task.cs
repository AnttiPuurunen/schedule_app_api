using System;
using System.Collections.Generic;

namespace ScheduleAppApi.Models;

public partial class Task
{
    public int Taskid { get; set; }

    public string Name { get; set; }

    public int? Tasktypeid { get; set; }

    public DateTime? Duedate { get; set; }

    public sbyte? Iscompleted { get; set; }

    public virtual Tasktype Tasktype { get; set; }
}
