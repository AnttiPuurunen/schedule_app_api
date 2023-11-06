using Microsoft.AspNetCore.Mvc;
using ScheduleAppApi.Models;

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
    public IEnumerable<ScheduleAppApi.Models.Task> Get()
    {
        return _scheduleContext.Tasks.ToList();
    }
}
