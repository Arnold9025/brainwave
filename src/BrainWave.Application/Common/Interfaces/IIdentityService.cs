namespace BrainWave.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<(bool Succeeded, string? Token, string? Error)> RegisterAsync(string username, string email, string password);
    Task<(bool Succeeded, string? Token, string? Error)> LoginAsync(string email, string password);
    Task<string?> GetUserNameAsync(Guid userId);
}
