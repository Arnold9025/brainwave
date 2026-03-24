using BrainWave.Application.Common.Interfaces;
using BrainWave.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrainWave.Infrastructure.Persistence;

public class BrainWaveDbContext : DbContext, IBrainWaveDbContext
{
    public BrainWaveDbContext(DbContextOptions<BrainWaveDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Goal> Goals => Set<Goal>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<ProductivityLog> ProductivityLogs => Set<ProductivityLog>();
    public DbSet<AISuggestion> AISuggestions => Set<AISuggestion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        
        // Goals -> Tasks: if a Goal is deleted, its Tasks could be deleted (or let GoalId be null). Setting Cascade.
        modelBuilder.Entity<Goal>()
            .HasMany(g => g.Tasks)
            .WithOne(t => t.Goal)
            .HasForeignKey(t => t.GoalId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> Goals
        modelBuilder.Entity<User>()
            .HasMany(u => u.Goals)
            .WithOne(g => g.User)
            .HasForeignKey(g => g.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> Tasks
        modelBuilder.Entity<User>()
            .HasMany(u => u.Tasks)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // User -> ProductivityLogs
        modelBuilder.Entity<User>()
            .HasMany(u => u.ProductivityLogs)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // User -> AISuggestions
        modelBuilder.Entity<User>()
            .HasMany(u => u.AISuggestions)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        base.OnModelCreating(modelBuilder);
    }
}
