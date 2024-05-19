using Microsoft.AspNetCore.Mvc;
using ScheduleAppApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ScheduleAppApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasktypeController : ControllerBase
{
    private readonly ILogger<TaskController> _logger;
    private ScheduleContext _scheduleContext;

    public TasktypeController(ILogger<TaskController> logger, ScheduleContext scheduleContext)
    {
        _logger = logger;
        _scheduleContext = scheduleContext;
    }

    [HttpGet]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<Tasktype>>> GetTasktypes()
    {   
        // Get all itemtypes
        var items = await _scheduleContext.Tasktypes.ToListAsync();
        
        if (items.Count == 0 || items == null)
        {
            return NotFound();
        }

        return Ok(items);
    }
}
