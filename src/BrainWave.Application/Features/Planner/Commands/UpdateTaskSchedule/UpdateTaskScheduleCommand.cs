using BrainWave.Application.Common.Interfaces;
using MediatR;

namespace BrainWave.Application.Features.Planner.Commands.UpdateTaskSchedule;

public record UpdateTaskScheduleCommand(Guid Id, Guid UserId, DateTime ScheduledAt) : IRequest<bool>;

public class UpdateTaskScheduleCommandHandler : IRequestHandler<UpdateTaskScheduleCommand, bool>
{
    private readonly IBrainWaveDbContext _context;

    public UpdateTaskScheduleCommandHandler(IBrainWaveDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateTaskScheduleCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks.FindAsync(new object[] { request.Id }, cancellationToken);

        if (task == null || task.UserId != request.UserId) 
            return false;

        task.ScheduledAt = request.ScheduledAt;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
