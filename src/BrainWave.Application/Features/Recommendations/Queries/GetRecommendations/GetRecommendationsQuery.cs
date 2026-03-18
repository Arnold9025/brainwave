using BrainWave.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrainWave.Application.Features.Recommendations.Queries.GetRecommendations;

public record GetRecommendationsQuery(Guid UserId) : IRequest<List<string>>;

public class GetRecommendationsQueryHandler : IRequestHandler<GetRecommendationsQuery, List<string>>
{
    private readonly IBrainWaveDbContext _context;

    public GetRecommendationsQueryHandler(IBrainWaveDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> Handle(GetRecommendationsQuery request, CancellationToken cancellationToken)
    {
        var recommendations = new List<string>();
        
        var user = await _context.Users
            .Include(u => u.Tasks)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null) return recommendations;

        var pendingHighPriority = user.Tasks.Count(t => !t.IsCompleted && t.Priority == 3);
        if (pendingHighPriority > 0)
        {
            recommendations.Add($"Tu as {pendingHighPriority} tâches prioritaires en attente. Concentre-toi dessus !");
        }

        if (user.ProductivityScore < 50)
        {
            recommendations.Add("Ton score est un peu bas. Essaie de compléter de petites tâches pour reprendre le rythme.");
        }
        else
        {
            recommendations.Add("Excellent travail ! Continue sur cette lancée.");
        }

        return recommendations;
    }
}
