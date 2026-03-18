using BrainWave.Application.Common.Interfaces;
using BrainWave.Application.Features.Tasks.Commands.CreateTask;
using BrainWave.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Xunit;

namespace BrainWave.Application.UnitTests.Features.Tasks.Commands.CreateTask;

public class CreateTaskCommandHandlerTests
{
    private readonly IBrainWaveDbContext _context;
    private readonly CreateTaskCommandHandler _handler;

    public CreateTaskCommandHandlerTests()
    {
        _context = Substitute.For<IBrainWaveDbContext>();
        _handler = new CreateTaskCommandHandler(_context);
    }

    [Fact]
    public async Task Handle_Should_AddNewTask_And_ReturnGuid()
    {
        // Arrange
        var command = new CreateTaskCommand
        {
            Title = "Test Task",
            Description = "Test Description",
            Priority = 2,
            UserId = Guid.NewGuid()
        };

        var mockDbSet = Substitute.For<DbSet<TaskItem>, IQueryable<TaskItem>>();
        _context.Tasks.Returns(mockDbSet);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        _context.Tasks.Received(1).Add(Arg.Is<TaskItem>(t => t.Title == command.Title));
        await _context.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
