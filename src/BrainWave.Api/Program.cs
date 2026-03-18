using BrainWave.Application;
using BrainWave.Infrastructure;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Automatically apply migrations on startup (Convenient for small deployments)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BrainWave.Infrastructure.Persistence.BrainWaveDbContext>();
    await Microsoft.EntityFrameworkCore.RelationalDatabaseFacadeExtensions.MigrateAsync(context.Database);
}

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.MapScalarApiReference();
app.MapGet("/", () => Results.Redirect("/scalar/v1"));
app.MapGet("/swagger", () => Results.Redirect("/scalar/v1"));

// app.UseHttpsRedirection(); // Uncomment if your host manages HTTPS internally

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
