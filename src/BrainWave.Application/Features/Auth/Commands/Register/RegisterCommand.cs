using BrainWave.Application.Common.Interfaces;
using MediatR;

namespace BrainWave.Application.Features.Auth.Commands.Register;

public record RegisterCommand(string Username, string Email, string Password) : IRequest<(bool Succeeded, string? Token, string? Error)>;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, (bool Succeeded, string? Token, string? Error)>
{
    private readonly IIdentityService _identityService;

    public RegisterCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<(bool Succeeded, string? Token, string? Error)> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.RegisterAsync(request.Username, request.Email, request.Password);
    }
}
