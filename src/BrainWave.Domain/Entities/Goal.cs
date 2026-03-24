using BrainWave.Domain.Common;

namespace BrainWave.Domain.Entities;

public class Goal : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? Deadline { get; set; }
    public int Priority { get; set; }
    public string Status { get; set; } = "To Do";
    
    // Relationships
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
