using BrainWave.Application.Features.Home.Queries.GetDashboard;
using Microsoft.AspNetCore.Mvc;

namespace BrainWave.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ApiControllerBase
{
    [HttpGet("dashboard")]
    public async Task<ActionResult<DashboardDto>> GetDashboard([FromQuery] Guid userId)
    {
        return await Mediator.Send(new GetDashboardQuery(userId));
    }
}
