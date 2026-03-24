using BrainWave.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrainWave.Application.Features.Planner.Commands.AutoSchedule;

public record AutoScheduleCommand(Guid UserId, DateTime TargetDate) : IRequest<bool>;

public class AutoScheduleCommandHandler : IRequestHandler<AutoScheduleCommand, bool>
{
    private readonly IBrainWaveDbContext _context;

    public AutoScheduleCommandHandler(IBrainWaveDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(AutoScheduleCommand request, CancellationToken cancellationToken)
    {
        // Simple algo: get all unscheduled to-do tasks for the user and schedule them
        // prioritizing High > Medium > Low
        
        var unscheduledTasks = await _context.Tasks
            .Where(t => t.UserId == request.UserId && t.Status != "Completed" && t.ScheduledAt == null)
            .OrderByDescending(t => t.Priority)
            .ToListAsync(cancellationToken);

        if (!unscheduledTasks.Any()) return true;

        var currentSlot = request.TargetDate.Date.AddHours(9); // Start at 9 AM

        foreach (var task in unscheduledTasks)
        {
            task.ScheduledAt = currentSlot;
            
            // Add duration or default 60 mins
            var duration = task.EstimatedDuration > 0 ? task.EstimatedDuration : 60;
            currentSlot = currentSlot.AddMinutes(duration);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
