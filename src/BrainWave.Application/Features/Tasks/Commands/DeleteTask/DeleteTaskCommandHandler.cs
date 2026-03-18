using BrainWave.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrainWave.Application.Features.Tasks.Commands.DeleteTask;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
{
    private readonly IBrainWaveDbContext _context;

    public DeleteTaskCommandHandler(IBrainWaveDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (entity == null) return false;

        _context.Tasks.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
