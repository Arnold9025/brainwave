using BrainWave.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrainWave.Application.Features.Home.Queries.GetDashboard;

public record GetDashboardQuery(Guid UserId) : IRequest<DashboardDto>;

public record DashboardDto
{
    public double ProductivityScore { get; init; }
    public List<TaskItemDto> TodayTasks { get; init; } = new();
    public List<AISuggestionDto> AISuggestions { get; init; } = new();
}

public record TaskItemDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public int Priority { get; init; }
    public DateTime? ScheduledAt { get; init; }
}

public record AISuggestionDto
{
    public Guid Id { get; init; }
    public string Content { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
}

public class GetDashboardQueryHandler : IRequestHandler<GetDashboardQuery, DashboardDto>
{
    private readonly IBrainWaveDbContext _context;

    public GetDashboardQueryHandler(IBrainWaveDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Tasks)
            .Include(u => u.AISuggestions)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null) return new DashboardDto();

        var today = DateTime.UtcNow.Date;

        return new DashboardDto
        {
            ProductivityScore = user.ProductivityScore,
            TodayTasks = user.Tasks
                .Where(t => t.ScheduledAt?.Date == today)
                .Select(t => new TaskItemDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Status = t.Status,
                    Priority = t.Priority,
                    ScheduledAt = t.ScheduledAt
                }).ToList(),
            AISuggestions = user.AISuggestions
                .Where(s => !s.IsAccepted)
                .Select(s => new AISuggestionDto
                {
                    Id = s.Id,
                    Content = s.Content,
                    Type = s.Type
                }).ToList()
        };
    }
}
