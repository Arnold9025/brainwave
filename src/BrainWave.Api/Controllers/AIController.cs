using BrainWave.Application.Features.AI.Commands.AcceptSuggestion;
using BrainWave.Application.Features.AI.Commands.GenerateAIPlan;
using BrainWave.Application.Features.AI.Commands.GenerateSuggestions;
using BrainWave.Application.Features.AI.Queries.GetSuggestions;
using Microsoft.AspNetCore.Mvc;

namespace BrainWave.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AIController : ApiControllerBase
{
    [HttpPost("plan")]
    public async Task<ActionResult<List<Guid>>> GeneratePlan(GenerateAIPlanCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("suggestions/generate")]
    public async Task<ActionResult<bool>> GenerateSuggestions(GenerateSuggestionsCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpGet("suggestions")]
    public async Task<ActionResult<List<SuggestionDto>>> GetSuggestions([FromQuery] Guid userId)
    {
        return await Mediator.Send(new GetSuggestionsQuery(userId));
    }

    [HttpPost("suggestions/{id}/accept")]
    public async Task<ActionResult<bool>> AcceptSuggestion(Guid id, AcceptSuggestionCommand command)
    {
        if (id != command.Id) return BadRequest();
        var success = await Mediator.Send(command);
        if (!success) return NotFound();
        return Ok(success);
    }
}
