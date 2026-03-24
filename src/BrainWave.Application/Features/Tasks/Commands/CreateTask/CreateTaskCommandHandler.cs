using BrainWave.Application.Common.Interfaces;
using BrainWave.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BrainWave.Application.Features.Tasks.Commands.CreateTask;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly IBrainWaveDbContext _context;
    private readonly ILogger<CreateTaskCommandHandler> _logger;

    public CreateTaskCommandHandler(IBrainWaveDbContext context, ILogger<CreateTaskCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling CreateTask for UserId: {UserId}", request.UserId);
        
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

        var entity = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            EstimatedDuration = request.EstimatedDuration,
            GoalId = request.GoalId,
            UserId = request.UserId
        };

        _context.Tasks.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
