using BrainWave.Application.Common.Interfaces;
using BrainWave.Domain.Entities;
using MediatR;

namespace BrainWave.Application.Features.Productivity.Commands.EndFocusSession;

public record EndFocusSessionCommand(Guid UserId, Guid? TaskId, int DurationMinutes) : IRequest<bool>;

public class EndFocusSessionCommandHandler : IRequestHandler<EndFocusSessionCommand, bool>
{
    private readonly IBrainWaveDbContext _context;
    private readonly ICalculateScoreService _scoreService;

    public EndFocusSessionCommandHandler(IBrainWaveDbContext context, ICalculateScoreService scoreService)
    {
        _context = context;
        _scoreService = scoreService;
    }

    public async Task<bool> Handle(EndFocusSessionCommand request, CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow.Date;
        
        var log = new ProductivityLog
        {
            UserId = request.UserId,
            Date = today,
            FocusTime = request.DurationMinutes
        };
        
        _context.ProductivityLogs.Add(log);
        await _context.SaveChangesAsync(cancellationToken);
        
        await _scoreService.UpdateUserScoreAsync(request.UserId, cancellationToken);

        return true;
    }
}
