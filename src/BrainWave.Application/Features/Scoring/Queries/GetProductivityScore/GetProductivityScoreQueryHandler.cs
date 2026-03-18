using BrainWave.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrainWave.Application.Features.Scoring.Queries.GetProductivityScore;

public class GetProductivityScoreQueryHandler : IRequestHandler<GetProductivityScoreQuery, double>
{
    private readonly IBrainWaveDbContext _context;

    public GetProductivityScoreQueryHandler(IBrainWaveDbContext context)
    {
        _context = context;
    }

    public async Task<double> Handle(GetProductivityScoreQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        return user?.ProductivityScore ?? 0;
    }
}
