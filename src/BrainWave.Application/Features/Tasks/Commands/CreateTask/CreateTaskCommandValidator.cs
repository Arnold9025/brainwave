using FluentValidation;

namespace BrainWave.Application.Features.Tasks.Commands.CreateTask;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.UserId)
            .NotEmpty();
        
        RuleFor(v => v.Priority)
            .InclusiveBetween(1, 3);
    }
}
