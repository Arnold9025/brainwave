using BrainWave.Application.Common.Interfaces;
using BrainWave.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BrainWave.Application.Features.Objectives.Commands.CreateObjective;

public class CreateObjectiveCommandHandler : IRequestHandler<CreateObjectiveCommand, Guid>
{
    private readonly IBrainWaveDbContext _context;
    private readonly ILogger<CreateObjectiveCommandHandler> _logger;

    public CreateObjectiveCommandHandler(IBrainWaveDbContext context, ILogger<CreateObjectiveCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Guid> Handle(CreateObjectiveCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateObjective for UserId: {UserId}", request.UserId);
        
        // Ensure user exists for development/testing
        var user = await _context.Users.FindAsync([request.UserId], cancellationToken);
        if (user == null)
        {
            _logger.LogWarning("User {UserId} not found. Creating temporary user.", request.UserId);
            user = new User 
            { 
                Id = request.UserId, 
                Username = "TestUser", 
                Email = "test@example.com" 
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("User {UserId} created successfully.", request.UserId);
        }

        var entity = new Objective
        {
            Title = request.Title,
            Description = request.Description,
            Deadline = request.Deadline,
            UserId = request.UserId
        };

        _context.Objectives.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
