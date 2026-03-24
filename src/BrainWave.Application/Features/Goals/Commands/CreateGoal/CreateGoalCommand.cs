using BrainWave.Application.Common.Interfaces;
using BrainWave.Domain.Entities;
using MediatR;

namespace BrainWave.Application.Features.Goals.Commands.CreateGoal;

public record CreateGoalCommand : IRequest<Guid>
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DateTime? Deadline { get; init; }
    public int Priority { get; init; }
    public Guid UserId { get; init; }
}

public class CreateGoalCommandHandler : IRequestHandler<CreateGoalCommand, Guid>
{
    private readonly IBrainWaveDbContext _context;

    public CreateGoalCommandHandler(IBrainWaveDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = new Goal
        {
            Title = request.Title,
            Description = request.Description,
            Deadline = request.Deadline,
            Priority = request.Priority,
            Status = "To Do",
            UserId = request.UserId
        };

        _context.Goals.Add(goal);
        await _context.SaveChangesAsync(cancellationToken);

        return goal.Id;
    }
}
