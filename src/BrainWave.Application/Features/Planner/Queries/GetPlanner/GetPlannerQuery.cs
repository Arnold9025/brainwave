using BrainWave.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrainWave.Application.Features.Planner.Queries.GetPlanner;

public record GetPlannerQuery(Guid UserId, DateTime StartDate, DateTime EndDate) : IRequest<List<PlannerTaskDto>>;

public record PlannerTaskDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public DateTime? ScheduledAt { get; init; }
    public int EstimatedDuration { get; init; }
    public int Priority { get; init; }
}

public class GetPlannerQueryHandler : IRequestHandler<GetPlannerQuery, List<PlannerTaskDto>>
{
    private readonly IBrainWaveDbContext _context;

    public GetPlannerQueryHandler(IBrainWaveDbContext context)
    {
        _context = context;
    }

    public async Task<List<PlannerTaskDto>> Handle(GetPlannerQuery request, CancellationToken cancellationToken)
    {
        return await _context.Tasks
            .Where(t => t.UserId == request.UserId && 
                        t.ScheduledAt >= request.StartDate && 
                        t.ScheduledAt <= request.EndDate)
            .Select(t => new PlannerTaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Status = t.Status,
                ScheduledAt = t.ScheduledAt,
                EstimatedDuration = t.EstimatedDuration,
                Priority = t.Priority
            })
            .OrderBy(t => t.ScheduledAt)
            .ToListAsync(cancellationToken);
    }
}
