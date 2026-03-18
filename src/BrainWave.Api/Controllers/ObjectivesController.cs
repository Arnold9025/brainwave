using BrainWave.Application.Features.Objectives.Commands.CreateObjective;
using BrainWave.Application.Features.Objectives.Queries.GetObjectives;
using Microsoft.AspNetCore.Mvc;

namespace BrainWave.Api.Controllers;

public class ObjectivesController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ObjectiveDto>>> GetObjectives([FromQuery] Guid userId)
    {
        return await Mediator.Send(new GetObjectivesQuery { UserId = userId });
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateObjectiveCommand command)
    {
        return await Mediator.Send(command);
    }
}
