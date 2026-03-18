using MediatR;

namespace BrainWave.Application.Features.Tasks.Commands.DeleteTask;

public record DeleteTaskCommand(Guid Id) : IRequest<bool>;
