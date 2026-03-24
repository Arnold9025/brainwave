using BrainWave.Domain.Common;

namespace BrainWave.Domain.Entities;

public class User : Entity
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public double ProductivityScore { get; set; }
    
    // Relationships
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    public ICollection<Goal> Goals { get; set; } = new List<Goal>();
    public ICollection<ProductivityLog> ProductivityLogs { get; set; } = new List<ProductivityLog>();
    public ICollection<AISuggestion> AISuggestions { get; set; } = new List<AISuggestion>();
}
