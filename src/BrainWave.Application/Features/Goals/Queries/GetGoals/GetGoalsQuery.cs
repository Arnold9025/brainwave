using BrainWave.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrainWave.Application.Features.Goals.Queries.GetGoals;

public record GetGoalsQuery(Guid UserId) : IRequest<List<GoalDto>>;

public record GoalDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime? Deadline { get; init; }
    public int Priority { get; init; }
    public string Status { get; init; } = string.Empty;
}

public class GetGoalsQueryHandler : IRequestHandler<GetGoalsQuery, List<GoalDto>>
{
    private readonly IBrainWaveDbContext _context;

    public GetGoalsQueryHandler(IBrainWaveDbContext context)
    {
        _context = context;
    }

    public async Task<List<GoalDto>> Handle(GetGoalsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Goals
            .Where(g => g.UserId == request.UserId)
            .Select(g => new GoalDto
            {
                Id = g.Id,
                Title = g.Title,
                Description = g.Description,
                Deadline = g.Deadline,
                Priority = g.Priority,
                Status = g.Status
            })
            .ToListAsync(cancellationToken);
    }
}
