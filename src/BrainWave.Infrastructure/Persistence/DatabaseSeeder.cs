using BrainWave.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace BrainWave.Infrastructure.Persistence;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(BrainWaveDbContext context)
    {
        if (!await context.Users.AnyAsync())
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "testuser",
                Email = "testuser@brainwave.test",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                ProductivityScore = 50.0,
                CreatedAt = DateTime.UtcNow
            };

            var goal = new Goal
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Title = "Launch MVP",
                Description = "Complete all core features and release the first version of the platform.",
                Deadline = DateTime.UtcNow.AddDays(30),
                Priority = 3,
                Status = "In Progress",
                CreatedAt = DateTime.UtcNow
            };

            var task1 = new TaskItem
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                GoalId = goal.Id,
                Title = "Database Architecture",
                Description = "Design the PostgreSQL schema and create initial migrations.",
                Status = "Completed",
                Priority = 3,
                EstimatedDuration = 120,
                CompletedAt = DateTime.UtcNow.AddDays(-1),
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            };

            var task2 = new TaskItem
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                GoalId = goal.Id,
                Title = "API Authentication",
                Description = "Implement JWT token generation and validation middleware.",
                Status = "In Progress",
                Priority = 3,
                EstimatedDuration = 180,
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            };

            var task3 = new TaskItem
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                GoalId = goal.Id,
                Title = "Frontend Integration",
                Description = "Connect the Flutter dashboard to the tasks API.",
                Status = "To Do",
                Priority = 2,
                EstimatedDuration = 240,
                CreatedAt = DateTime.UtcNow
            };

            var productivityLog = new ProductivityLog
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Date = DateTime.UtcNow.Date,
                FocusTime = 120,
                CompletedTasks = 3,
                Score = 50.0,
                CreatedAt = DateTime.UtcNow
            };

            await context.Users.AddAsync(user);
            await context.Goals.AddAsync(goal);
            await context.Tasks.AddRangeAsync(task1, task2, task3);
            await context.ProductivityLogs.AddAsync(productivityLog);

            await context.SaveChangesAsync();
        }
    }
}
