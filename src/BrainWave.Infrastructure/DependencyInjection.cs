using BrainWave.Application.Common.Interfaces;
using BrainWave.Infrastructure.Identity;
using BrainWave.Infrastructure.Persistence;
using BrainWave.Infrastructure.Services;
using Hangfire;
using Hangfire.PostgreSql;
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
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IAIService, OpenAIService>();

        services.AddHangfire(cfg => cfg
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(o => o.UseNpgsqlConnection(configuration.GetConnectionString("DefaultConnection"))));

        services.AddHangfireServer();

        return services;
    }
}
