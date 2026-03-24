using BrainWave.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrainWave.Application.Features.Goals.Queries.GetGoalDetails;

public record GetGoalDetailsQuery(Guid Id, Guid UserId) : IRequest<GoalDetailsDto?>;

public record GoalDetailsDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime? Deadline { get; init; }
    public int Priority { get; init; }
    public string Status { get; init; } = string.Empty;
    public List<TaskItemDto> Tasks { get; init; } = new();
}

public record TaskItemDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
}

public class GetGoalDetailsQueryHandler : IRequestHandler<GetGoalDetailsQuery, GoalDetailsDto?>
{
    private readonly IBrainWaveDbContext _context;

    public GetGoalDetailsQueryHandler(IBrainWaveDbContext context)
    {
        _context = context;
    }

    public async Task<GoalDetailsDto?> Handle(GetGoalDetailsQuery request, CancellationToken cancellationToken)
    {
        var goal = await _context.Goals
            .Include(g => g.Tasks)
            .FirstOrDefaultAsync(g => g.Id == request.Id && g.UserId == request.UserId, cancellationToken);

        if (goal == null) return null;

        return new GoalDetailsDto
        {
            Id = goal.Id,
            Title = goal.Title,
            Description = goal.Description,
            Deadline = goal.Deadline,
            Priority = goal.Priority,
            Status = goal.Status,
            Tasks = goal.Tasks.Select(t => new TaskItemDto
            {
                Id = t.Id,
                Title = t.Title,
                Status = t.Status
            }).ToList()
        };
    }
}
