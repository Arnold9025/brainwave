using BrainWave.Application.Common.Interfaces;
using BrainWave.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrainWave.Infrastructure.Persistence;

public class BrainWaveDbContext : DbContext, IBrainWaveDbContext
{
    public BrainWaveDbContext(DbContextOptions<BrainWaveDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Objective> Objectives => Set<Objective>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BrainWaveDbContext).Assembly);
        
        // Example configuration directly here if needed
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        
        base.OnModelCreating(modelBuilder);
    }
}
