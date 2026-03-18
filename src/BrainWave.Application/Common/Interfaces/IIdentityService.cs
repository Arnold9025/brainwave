namespace BrainWave.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(Guid userId);
    Task<(bool IsSuccess, Guid UserId)> CreateUserAsync(string userName, string email, string password);
    Task<bool> CheckPasswordAsync(Guid userId, string password);
}
