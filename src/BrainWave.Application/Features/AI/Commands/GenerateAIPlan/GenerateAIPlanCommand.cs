using BrainWave.Application.Common.Interfaces;
using BrainWave.Domain.Entities;
using MediatR;

namespace BrainWave.Application.Features.AI.Commands.GenerateAIPlan;

public record GenerateAIPlanCommand(Guid GoalId, Guid UserId) : IRequest<List<Guid>>;

public class GenerateAIPlanCommandHandler : IRequestHandler<GenerateAIPlanCommand, List<Guid>>
{
    private readonly IBrainWaveDbContext _context;
    private readonly IAIService _aiService;

    public GenerateAIPlanCommandHandler(IBrainWaveDbContext context, IAIService aiService)
    {
        _context = context;
        _aiService = aiService;
    }

    public async Task<List<Guid>> Handle(GenerateAIPlanCommand request, CancellationToken cancellationToken)
    {
        var goal = await _context.Goals.FindAsync(new object[] { request.GoalId }, cancellationToken);

        if (goal == null || goal.UserId != request.UserId)
            return new List<Guid>();

        var generatedTasks = await _aiService.GeneratePlanAsync(goal, cancellationToken);

        var taskIds = new List<Guid>();

        foreach (var task in generatedTasks)
        {
            task.GoalId = goal.Id;
            task.UserId = request.UserId;
            task.Status = "To Do";
            
            _context.Tasks.Add(task);
            taskIds.Add(task.Id);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return taskIds;
    }
}
