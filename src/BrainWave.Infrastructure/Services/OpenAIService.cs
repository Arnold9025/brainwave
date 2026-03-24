using BrainWave.Application.Common.Interfaces;
using BrainWave.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BrainWave.Infrastructure.Services;

public class OpenAIService : IAIService
{
    private readonly ILogger<OpenAIService> _logger;

    public OpenAIService(ILogger<OpenAIService> logger)
    {
        _logger = logger;
    }

    public async Task<List<TaskItem>> GeneratePlanAsync(Goal goal, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Generating AI plan for Goal: {GoalId}", goal.Id);
        await Task.Delay(500, cancellationToken);
        
        return new List<TaskItem>
        {
            new TaskItem { Title = $"AI Step 1 for {goal.Title}", Description = "Auto-generated", Priority = 3, EstimatedDuration = 30 },
            new TaskItem { Title = $"AI Step 2 for {goal.Title}", Description = "Auto-generated", Priority = 2, EstimatedDuration = 45 }
        };
    }

    public async Task<string> GenerateSuggestionAsync(User user, List<TaskItem> recentTasks, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Generating AI suggestion for User: {UserId}", user.Id);
        await Task.Delay(500, cancellationToken);
        return "Focus on high-priority tasks in the morning for better productivity.";
    }
}
