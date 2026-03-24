using BrainWave.Application.Features.Productivity.Commands.EndFocusSession;
using BrainWave.Application.Features.Productivity.Commands.StartFocusSession;
using BrainWave.Application.Features.Productivity.Queries.GetProductivity;
using Microsoft.AspNetCore.Mvc;

namespace BrainWave.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductivityController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ProductivityDto>> GetProductivity([FromQuery] Guid userId)
    {
        return await Mediator.Send(new GetProductivityQuery(userId));
    }

    [HttpPost("focus/start")]
    public async Task<ActionResult<bool>> StartFocus(StartFocusSessionCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("focus/end")]
    public async Task<ActionResult<bool>> EndFocus(EndFocusSessionCommand command)
    {
        return await Mediator.Send(command);
    }
}
