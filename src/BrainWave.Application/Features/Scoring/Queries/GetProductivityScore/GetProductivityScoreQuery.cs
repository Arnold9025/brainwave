using MediatR;

namespace BrainWave.Application.Features.Scoring.Queries.GetProductivityScore;

public record GetProductivityScoreQuery(Guid UserId) : IRequest<double>;
