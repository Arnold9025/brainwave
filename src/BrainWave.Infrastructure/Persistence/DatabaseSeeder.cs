using BrainWave.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace BrainWave.Infrastructure.Persistence;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(BrainWaveDbContext context)
    {
        if (!await context.Users.AnyAsync(u => u.Email == "arnold@brainwave.test"))
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "Arnold",
                Email = "arnold@brainwave.test",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                ProductivityScore = 78.0,
                CreatedAt = DateTime.UtcNow
            };
            
            // Goals
            var goal = new Goal
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Title = "Q3 Marketing",
                Description = "Finish Q3 marketing sprint.",
                Deadline = DateTime.UtcNow.AddDays(8), // Next milestone 8 days
                Priority = 3,
                Status = "In Progress",
                CreatedAt = DateTime.UtcNow
            };

            // Tasks
            var task1 = new TaskItem
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                GoalId = goal.Id,
                Title = "Complete API documentation for backend project",
                Description = "Write Swagger docs and endpoint details.",
                Status = "In Progress",
                Priority = 3,
                EstimatedDuration = 120,
                ScheduledAt = DateTime.UtcNow.Date.AddHours(9), // 9:00 AM
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            };

            var task2 = new TaskItem
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                GoalId = null,
                Title = "60-minute deep work session on architecture",
                Description = "Daily goal architecture review.",
                Status = "To Do",
                Priority = 1,
                EstimatedDuration = 60,
                ScheduledAt = DateTime.UtcNow.Date.AddHours(16).AddMinutes(30), // 4:30 PM
                CreatedAt = DateTime.UtcNow
            };

            var task3 = new TaskItem
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                GoalId = goal.Id,
                Title = "Review pull requests",
                Description = "Review teammates code.",
                Status = "To Do",
                Priority = 2,
                EstimatedDuration = 45,
                ScheduledAt = DateTime.UtcNow.Date.AddHours(14),
                CreatedAt = DateTime.UtcNow
            };

            // AI Insights
            var insight1 = new AISuggestion
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Content = "You're 2 days behind on the Q3 Marketing sprint. I recommend shifting Friday's tasks to clear the block.",
                Type = "Optimization Hint",
                IsAccepted = false,
                CreatedAt = DateTime.UtcNow
            };

            var insight2 = new AISuggestion
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Content = "Your peak window is starting. Deep Focus blocks distractions.",
                Type = "Focus Alert",
                IsAccepted = false,
                CreatedAt = DateTime.UtcNow
            };

            // Productivity Log for Activity stats
            var productivityLog = new ProductivityLog
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Date = DateTime.UtcNow.Date,
                FocusTime = 252, // 4.2 hours
                CompletedTasks = 5, 
                Score = 78.0,
                CreatedAt = DateTime.UtcNow
            };

            await context.Users.AddAsync(user);
            await context.Goals.AddAsync(goal);
            await context.Tasks.AddRangeAsync(task1, task2, task3);
            await context.AISuggestions.AddRangeAsync(insight1, insight2);
            await context.ProductivityLogs.AddAsync(productivityLog);

            await context.SaveChangesAsync();
        }
    }
}
