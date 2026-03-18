using BrainWave.Application.Common.Interfaces;
using MediatR;

namespace BrainWave.Application.Features.Auth.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<(bool Succeeded, string? Token, string? Error)>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, (bool Succeeded, string? Token, string? Error)>
{
    private readonly IIdentityService _identityService;

    public LoginCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<(bool Succeeded, string? Token, string? Error)> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.LoginAsync(request.Email, request.Password);
    }
}
