using BrainWave.Application.Features.Goals.Commands.CreateGoal;
using BrainWave.Application.Features.Goals.Commands.DeleteGoal;
using BrainWave.Application.Features.Goals.Queries.GetGoalDetails;
using BrainWave.Application.Features.Goals.Queries.GetGoals;
using Microsoft.AspNetCore.Mvc;

namespace BrainWave.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalsController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateGoalCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet]
    public async Task<ActionResult<List<GoalDto>>> GetAll([FromQuery] Guid userId)
    {
        return await Mediator.Send(new GetGoalsQuery(userId));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GoalDetailsDto>> GetDetails(Guid id, [FromQuery] Guid userId)
    {
        var goal = await Mediator.Send(new GetGoalDetailsQuery(id, userId));
        if (goal == null) return NotFound();
        return Ok(goal);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, [FromQuery] Guid userId)
    {
        var success = await Mediator.Send(new DeleteGoalCommand(id, userId));
        if (!success) return NotFound();
        return NoContent();
    }
}
