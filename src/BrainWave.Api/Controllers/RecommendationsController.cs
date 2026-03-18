using BrainWave.Application.Features.Recommendations.Queries.GetRecommendations;
using Microsoft.AspNetCore.Mvc;

namespace BrainWave.Api.Controllers;

public class RecommendationsController : ApiControllerBase
{
    [HttpGet("{userId}")]
    public async Task<ActionResult<List<string>>> GetRecommendations(Guid userId)
    {
        return await Mediator.Send(new GetRecommendationsQuery(userId));
    }
}
