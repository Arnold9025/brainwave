using MediatR;

namespace BrainWave.Application.Features.Tasks.Commands.CreateTask;

public record CreateTaskCommand : IRequest<Guid>
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime? DueDate { get; init; }
    public int Priority { get; init; }
    public Guid UserId { get; init; }
}
