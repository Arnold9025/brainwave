using BrainWave.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrainWave.Application.Features.Scoring.Commands.RecalculateScore;

public record RecalculateScoreCommand(Guid UserId) : IRequest<double>;

public class RecalculateScoreCommandHandler : IRequestHandler<RecalculateScoreCommand, double>
{
    private readonly IBrainWaveDbContext _context;

    public RecalculateScoreCommandHandler(IBrainWaveDbContext context)
    {
        _context = context;
    }

    public async Task<double> Handle(RecalculateScoreCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Tasks)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null) return 0;

        var completedTasks = user.Tasks.Count(t => t.IsCompleted);
        var totalTasks = user.Tasks.Count;

        user.ProductivityScore = totalTasks > 0 
            ? (double)completedTasks / totalTasks * 100 
            : 0;

        await _context.SaveChangesAsync(cancellationToken);

        return user.ProductivityScore;
    }
}
