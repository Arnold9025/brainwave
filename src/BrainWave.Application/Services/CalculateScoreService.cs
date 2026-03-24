using BrainWave.Application.Common.Interfaces;
using BrainWave.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrainWave.Application.Services;

public class CalculateScoreService : ICalculateScoreService
{
    private readonly IBrainWaveDbContext _context;

    public CalculateScoreService(IBrainWaveDbContext context)
    {
        _context = context;
    }

    public async Task<double> CalculateDailyScoreAsync(Guid userId, DateTime date, CancellationToken cancellationToken)
    {
        var logs = await _context.ProductivityLogs
            .Where(l => l.UserId == userId && l.Date.Date == date.Date)
            .ToListAsync(cancellationToken);
            
        var tasksCompletedToday = await _context.Tasks
            .Where(t => t.UserId == userId && t.Status == "Completed" && t.CompletedAt >= date.Date && t.CompletedAt < date.Date.AddDays(1))
            .CountAsync(cancellationToken);

        var focusTime = logs.Sum(l => l.FocusTime);
        
        double completionPoints = tasksCompletedToday * 10; 
        double focusPoints = (focusTime / 60.0) * 20; 
        
        double score = completionPoints + focusPoints;
        return Math.Min(100, score);
    }

    public async Task UpdateUserScoreAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { userId }, cancellationToken);
        if (user == null) return;
        
        user.ProductivityScore = await CalculateDailyScoreAsync(userId, DateTime.UtcNow, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
