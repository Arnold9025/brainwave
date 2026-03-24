using BrainWave.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrainWave.Application.Features.Productivity.Queries.GetProductivity;

public record GetProductivityQuery(Guid UserId) : IRequest<ProductivityDto>;

public record ProductivityDto
{
    public double GlobalScore { get; init; }
    public List<DailyLogDto> History { get; init; } = new();
}

public record DailyLogDto
{
    public DateTime Date { get; init; }
    public int FocusTime { get; init; }
    public int CompletedTasks { get; init; }
    public double Score { get; init; }
}

public class GetProductivityQueryHandler : IRequestHandler<GetProductivityQuery, ProductivityDto>
{
    private readonly IBrainWaveDbContext _context;

    public GetProductivityQueryHandler(IBrainWaveDbContext context)
    {
        _context = context;
    }

    public async Task<ProductivityDto> Handle(GetProductivityQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.ProductivityLogs)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null) return new ProductivityDto();

        return new ProductivityDto
        {
            GlobalScore = user.ProductivityScore,
            History = user.ProductivityLogs
                .OrderByDescending(l => l.Date)
                .Take(7)
                .Select(l => new DailyLogDto
                {
                    Date = l.Date,
                    FocusTime = l.FocusTime,
                    CompletedTasks = l.CompletedTasks,
                    Score = l.Score
                }).ToList()
        };
    }
}
