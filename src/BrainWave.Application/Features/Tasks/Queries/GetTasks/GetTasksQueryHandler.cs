using BrainWave.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrainWave.Application.Features.Tasks.Queries.GetTasks;

public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, List<TaskDto>>
{
    private readonly IBrainWaveDbContext _context;

    public GetTasksQueryHandler(IBrainWaveDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        return await _context.Tasks
            .Where(t => t.UserId == request.UserId)
            .Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                ScheduledAt = t.ScheduledAt,
                CompletedAt = t.CompletedAt,
                Priority = t.Priority,
                EstimatedDuration = t.EstimatedDuration
            })
            .ToListAsync(cancellationToken);
    }
}
