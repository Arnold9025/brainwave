using MediatR;

namespace BrainWave.Application.Features.Objectives.Commands.CreateObjective;

public record CreateObjectiveCommand : IRequest<Guid>
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime? Deadline { get; init; }
    public Guid UserId { get; init; }
}
