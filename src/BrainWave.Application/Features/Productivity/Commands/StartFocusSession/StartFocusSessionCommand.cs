using MediatR;

namespace BrainWave.Application.Features.Productivity.Commands.StartFocusSession;

public record StartFocusSessionCommand(Guid UserId, Guid? TaskId) : IRequest<bool>;

public class StartFocusSessionCommandHandler : IRequestHandler<StartFocusSessionCommand, bool>
{
    public Task<bool> Handle(StartFocusSessionCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}
