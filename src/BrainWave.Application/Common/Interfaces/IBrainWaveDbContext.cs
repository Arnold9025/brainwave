using BrainWave.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrainWave.Application.Common.Interfaces;

public interface IBrainWaveDbContext
{
    DbSet<User> Users { get; }
    DbSet<Objective> Objectives { get; }
    DbSet<TaskItem> Tasks { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
