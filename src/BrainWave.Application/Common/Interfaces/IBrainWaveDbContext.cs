using BrainWave.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrainWave.Application.Common.Interfaces;

public interface IBrainWaveDbContext
{
    DbSet<User> Users { get; }
    DbSet<Goal> Goals { get; }
    DbSet<TaskItem> Tasks { get; }
    DbSet<ProductivityLog> ProductivityLogs { get; }
    DbSet<AISuggestion> AISuggestions { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
