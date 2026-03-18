using BrainWave.Application.Features.Scoring.Commands.RecalculateScore;
using BrainWave.Application.Features.Scoring.Queries.GetProductivityScore;
using Microsoft.AspNetCore.Mvc;

namespace BrainWave.Api.Controllers;

public class ScoringController : ApiControllerBase
{
    [HttpGet("{userId}")]
    public async Task<ActionResult<double>> GetScore(Guid userId)
    {
        return await Mediator.Send(new GetProductivityScoreQuery(userId));
    }

    [HttpPost("{userId}/recalculate")]
    public async Task<ActionResult<double>> Recalculate(Guid userId)
    {
        return await Mediator.Send(new RecalculateScoreCommand(userId));
    }
}
