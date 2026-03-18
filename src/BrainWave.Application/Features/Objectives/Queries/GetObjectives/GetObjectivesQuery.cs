using MediatR;

namespace BrainWave.Application.Features.Objectives.Queries.GetObjectives;

public record GetObjectivesQuery : IRequest<List<ObjectiveDto>>
{
    public Guid UserId { get; init; }
}

public record ObjectiveDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime? Deadline { get; init; }
    public bool IsCompleted { get; init; }
}
