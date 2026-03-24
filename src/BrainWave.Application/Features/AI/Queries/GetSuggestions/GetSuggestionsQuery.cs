using BrainWave.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BrainWave.Application.Features.AI.Queries.GetSuggestions;

public record GetSuggestionsQuery(Guid UserId) : IRequest<List<SuggestionDto>>;

public record SuggestionDto
{
    public Guid Id { get; init; }
    public string Content { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public bool IsAccepted { get; init; }
}

public class GetSuggestionsQueryHandler : IRequestHandler<GetSuggestionsQuery, List<SuggestionDto>>
{
    private readonly IBrainWaveDbContext _context;

    public GetSuggestionsQueryHandler(IBrainWaveDbContext context)
    {
        _context = context;
    }

    public async Task<List<SuggestionDto>> Handle(GetSuggestionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.AISuggestions
            .Where(s => s.UserId == request.UserId)
            .OrderByDescending(s => s.CreatedAt)
            .Select(s => new SuggestionDto
            {
                Id = s.Id,
                Content = s.Content,
                Type = s.Type,
                CreatedAt = s.CreatedAt,
                IsAccepted = s.IsAccepted
            })
            .ToListAsync(cancellationToken);
    }
}
