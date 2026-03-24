using BrainWave.Application.Features.Planner.Commands.AutoSchedule;
using BrainWave.Application.Features.Planner.Commands.UpdateTaskSchedule;
using BrainWave.Application.Features.Planner.Queries.GetPlanner;
using Microsoft.AspNetCore.Mvc;

namespace BrainWave.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlannerController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PlannerTaskDto>>> GetPlanner([FromQuery] Guid userId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        return await Mediator.Send(new GetPlannerQuery(userId, startDate, endDate));
    }

    [HttpPost("auto-schedule")]
    public async Task<ActionResult<bool>> AutoSchedule(AutoScheduleCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("tasks/{id}/schedule")]
    public async Task<ActionResult<bool>> UpdateSchedule(Guid id, UpdateTaskScheduleCommand command)
    {
        if (id != command.Id) return BadRequest();
        var success = await Mediator.Send(command);
        if (!success) return NotFound();
        return Ok(success);
    }
}
