using BrainWave.Domain.Common;

namespace BrainWave.Domain.Entities;

public class ProductivityLog : Entity
{
    public DateTime Date { get; set; }
    public int FocusTime { get; set; } // in minutes
    public int CompletedTasks { get; set; }
    public double Score { get; set; }
    
    // Relationships
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
