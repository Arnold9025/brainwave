using BrainWave.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrainWave.Application.Features.Tasks.Commands.UpdateTask;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, bool>
{
    private readonly IBrainWaveDbContext _context;

    public UpdateTaskCommandHandler(IBrainWaveDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (entity == null) return false;

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.Priority = request.Priority;
        entity.EstimatedDuration = request.EstimatedDuration;
        entity.ScheduledAt = request.ScheduledAt;
        entity.GoalId = request.GoalId;
        entity.Status = request.Status;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
