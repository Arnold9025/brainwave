using BrainWave.Application.Common.Events;
using BrainWave.Application.Common.Interfaces;
using MediatR;

namespace BrainWave.Application.Features.Events;

public class RecalculateScoreEventHandler : INotificationHandler<TaskCompletedEvent>
{
    private readonly ICalculateScoreService _scoreService;

    public RecalculateScoreEventHandler(ICalculateScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    public async Task Handle(TaskCompletedEvent notification, CancellationToken cancellationToken)
    {
        await _scoreService.UpdateUserScoreAsync(notification.UserId, cancellationToken);
    }
}
