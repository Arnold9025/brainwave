using BrainWave.Application.Common.Interfaces;
using BrainWave.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BrainWave.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BrainWaveDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(BrainWaveDbContext).Assembly.FullName)));

        services.AddScoped<IBrainWaveDbContext>(provider => provider.GetRequiredService<BrainWaveDbContext>());

        return services;
    }
}
