using BrainWave.Domain.Common;

namespace BrainWave.Domain.Entities;

public class AISuggestion : Entity
{
    public string Content { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // e.g., "Schedule", "Motivation", "Warning"
    public bool IsAccepted { get; set; }
    
    // Relationships
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
