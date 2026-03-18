namespace BrainWave.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateToken(Guid userId, string userName, string email);
}
