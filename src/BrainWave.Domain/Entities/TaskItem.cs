using BrainWave.Domain.Common;

namespace BrainWave.Domain.Entities;

public class TaskItem : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "To Do"; // e.g., To Do, In Progress, Completed
    public int Priority { get; set; } // 1: Low, 2: Medium, 3: High
    public int EstimatedDuration { get; set; } // In minutes
    public DateTime? ScheduledAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    
    // Relationships
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    
    public Guid? GoalId { get; set; }
    public Goal? Goal { get; set; }
}
