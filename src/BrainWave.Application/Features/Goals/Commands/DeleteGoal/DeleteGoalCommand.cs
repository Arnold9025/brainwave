using BrainWave.Application.Common.Interfaces;
using MediatR;

namespace BrainWave.Application.Features.Goals.Commands.DeleteGoal;

public record DeleteGoalCommand(Guid Id, Guid UserId) : IRequest<bool>;

public class DeleteGoalCommandHandler : IRequestHandler<DeleteGoalCommand, bool>
{
    private readonly IBrainWaveDbContext _context;

    public DeleteGoalCommandHandler(IBrainWaveDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteGoalCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Goals.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null || entity.UserId != request.UserId)
            return false;

        _context.Goals.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
