using BrainWave.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrainWave.Application.Features.Objectives.Queries.GetObjectives;

public class GetObjectivesQueryHandler : IRequestHandler<GetObjectivesQuery, List<ObjectiveDto>>
{
    private readonly IBrainWaveDbContext _context;

    public GetObjectivesQueryHandler(IBrainWaveDbContext context)
    {
        _context = context;
    }

    public async Task<List<ObjectiveDto>> Handle(GetObjectivesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Objectives
            .Where(o => o.UserId == request.UserId)
            .Select(o => new ObjectiveDto
            {
                Id = o.Id,
                Title = o.Title,
                Description = o.Description,
                Deadline = o.Deadline,
                IsCompleted = o.IsCompleted
            })
            .ToListAsync(cancellationToken);
    }
}
