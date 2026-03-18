using BrainWave.Domain.Common;

namespace BrainWave.Domain.Entities;

public class TaskItem : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public int Priority { get; set; } // 1: Low, 2: Medium, 3: High
    
    // Relationships
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    
    public Guid? ObjectiveId { get; set; }
    public Objective? Objective { get; set; }
}
