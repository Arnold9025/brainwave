using BrainWave.Domain.Entities;

namespace BrainWave.Application.Common.Interfaces;

public interface IAIService
{
    Task<List<TaskItem>> GeneratePlanAsync(Goal goal, CancellationToken cancellationToken);
    Task<string> GenerateSuggestionAsync(User user, List<TaskItem> recentTasks, CancellationToken cancellationToken);
}
