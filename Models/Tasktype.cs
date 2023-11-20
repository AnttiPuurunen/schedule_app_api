using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ScheduleAppApi.Models;

public partial class Tasktype
{
    public int TaskTypeId { get; set; }

    public string? Name { get; set; }
    
    [JsonIgnore]
    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
