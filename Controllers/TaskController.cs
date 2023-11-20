using Microsoft.AspNetCore.Mvc;
using ScheduleAppApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ScheduleAppApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ILogger<TaskController> _logger;
    private ScheduleContext _scheduleContext;

    public TaskController(ILogger<TaskController> logger, ScheduleContext scheduleContext)
    {
        _logger = logger;
        _scheduleContext = scheduleContext;
    }

    [HttpGet]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<Models.Task>>> Get()
    {
        List<Models.Task> Tasks = _scheduleContext.Tasks.ToList();

        var items = await _scheduleContext.Tasks.Include(x => x.Tasktype)
                                .OrderBy(x => x.Duedate)
                                .ToListAsync();

        if (items.Count == 0 || items == null)
            {
                return NotFound();
            }

        return Ok(items);
    }
}
