using BrainWave.Application.Common.Interfaces;
using MediatR;

namespace BrainWave.Application.Features.Tasks.Commands.CompleteTask;

public record CompleteTaskCommand(Guid Id, Guid UserId) : IRequest<bool>;

public class CompleteTaskCommandHandler : IRequestHandler<CompleteTaskCommand, bool>
{
    private readonly IBrainWaveDbContext _context;
    private readonly IPublisher _publisher;

    public CompleteTaskCommandHandler(IBrainWaveDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }

    public async Task<bool> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _context.Tasks.FindAsync(new object[] { request.Id }, cancellationToken);

        if (task == null || task.UserId != request.UserId) 
            return false;

        task.Status = "Completed";
        task.CompletedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new BrainWave.Application.Common.Events.TaskCompletedEvent(task.Id, task.UserId), cancellationToken);

        return true;
    }
}
