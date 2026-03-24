namespace BrainWave.Application.Common.Interfaces;

public interface ICalculateScoreService
{
    Task<double> CalculateDailyScoreAsync(Guid userId, DateTime date, CancellationToken cancellationToken);
    Task UpdateUserScoreAsync(Guid userId, CancellationToken cancellationToken);
}
