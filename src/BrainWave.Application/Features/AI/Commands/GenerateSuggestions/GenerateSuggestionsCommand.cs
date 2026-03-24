using BrainWave.Application.Common.Interfaces;
using BrainWave.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrainWave.Application.Features.AI.Commands.GenerateSuggestions;

public record GenerateSuggestionsCommand(Guid UserId) : IRequest<bool>;

public class GenerateSuggestionsCommandHandler : IRequestHandler<GenerateSuggestionsCommand, bool>
{
    private readonly IBrainWaveDbContext _context;
    private readonly IAIService _aiService;

    public GenerateSuggestionsCommandHandler(IBrainWaveDbContext context, IAIService aiService)
    {
        _context = context;
        _aiService = aiService;
    }

    public async Task<bool> Handle(GenerateSuggestionsCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { request.UserId }, cancellationToken);
        if (user == null) return false;

        var recentTasks = await _context.Tasks
            .Where(t => t.UserId == request.UserId)
            .OrderByDescending(t => t.CreatedAt)
            .Take(10)
            .ToListAsync(cancellationToken);

        var suggestionContent = await _aiService.GenerateSuggestionAsync(user, recentTasks, cancellationToken);

        if (!string.IsNullOrEmpty(suggestionContent))
        {
            var suggestion = new AISuggestion
            {
                UserId = request.UserId,
                Content = suggestionContent,
                Type = "General",
                IsAccepted = false
            };

            _context.AISuggestions.Add(suggestion);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return true;
    }
}
