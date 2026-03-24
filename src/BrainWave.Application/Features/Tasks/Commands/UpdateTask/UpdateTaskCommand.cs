using MediatR;

namespace BrainWave.Application.Features.Tasks.Commands.UpdateTask;

public record UpdateTaskCommand : IRequest<bool>
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public int Priority { get; init; }
    public int EstimatedDuration { get; init; }
    public DateTime? ScheduledAt { get; init; }
    public Guid? GoalId { get; init; }
    public string Status { get; init; } = string.Empty;
}
