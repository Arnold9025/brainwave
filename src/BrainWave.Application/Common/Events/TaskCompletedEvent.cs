using MediatR;

namespace BrainWave.Application.Common.Events;

public record TaskCompletedEvent(Guid TaskId, Guid UserId) : INotification;
