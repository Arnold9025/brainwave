using MediatR;

namespace BrainWave.Application.Features.Tasks.Queries.GetTasks;

public record GetTasksQuery : IRequest<List<TaskDto>>
{
    public Guid UserId { get; init; }
}

public record TaskDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime? DueDate { get; init; }
    public bool IsCompleted { get; init; }
    public int Priority { get; init; }
}
