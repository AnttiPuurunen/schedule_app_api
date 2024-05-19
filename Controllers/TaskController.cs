using Microsoft.AspNetCore.Mvc;
using ScheduleAppApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

    [HttpGet("tasksbydate/{duedate}")]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasksByDate(DateTime duedate)
    {
        List<Models.Task> Tasks = _scheduleContext.Tasks.ToList();

        DateTime duedatetime = duedate.Date;
        Console.WriteLine(duedatetime);

        var items = await _scheduleContext.Tasks.Where(x => x.Duedate == duedatetime)
                                .Include(x => x.Tasktype)
                                .ToListAsync();

        if (items.Count == 0 || items == null)
        {
            return NotFound();
        }

        return Ok(items);
    }

    // POST: api/task
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [ActionName(nameof(Models.Task))]
    public async Task<ActionResult<Models.Task>> PostItem([FromBody] Models.Task item)
    {
        var itemCreate = new Models.Task
        {
            Name = item.Name,
            Tasktypeid = item.Tasktypeid,
            Duedate = item.Duedate,
            Iscompleted = item.Iscompleted
        };

        if (!ModelState.IsValid)
            return BadRequest();

        _scheduleContext.Tasks.Add(itemCreate);

        try
        {
            await _scheduleContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

        return CreatedAtAction(nameof(Models.Task), new { Id = itemCreate.Taskid }, itemCreate);
    }
}
