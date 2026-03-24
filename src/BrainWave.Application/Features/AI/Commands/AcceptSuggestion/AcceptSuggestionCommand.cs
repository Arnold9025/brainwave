using BrainWave.Application.Common.Interfaces;
using BrainWave.Domain.Entities;
using MediatR;

namespace BrainWave.Application.Features.AI.Commands.AcceptSuggestion;

public record AcceptSuggestionCommand(Guid Id, Guid UserId) : IRequest<bool>;

public class AcceptSuggestionCommandHandler : IRequestHandler<AcceptSuggestionCommand, bool>
{
    private readonly IBrainWaveDbContext _context;

    public AcceptSuggestionCommandHandler(IBrainWaveDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(AcceptSuggestionCommand request, CancellationToken cancellationToken)
    {
        var suggestion = await _context.AISuggestions.FindAsync(new object[] { request.Id }, cancellationToken);

        if (suggestion == null || suggestion.UserId != request.UserId) return false;

        suggestion.IsAccepted = true;

        var newTask = new TaskItem
        {
            Title = "AI Suggested Task",
            Description = suggestion.Content,
            Priority = 2,
            Status = "To Do",
            UserId = request.UserId
        };

        _context.Tasks.Add(newTask);
        
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
