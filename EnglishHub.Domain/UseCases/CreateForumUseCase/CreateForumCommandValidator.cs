using FluentValidation;

namespace EnglishHub.Domain.UseCases.CreateForumUseCase;

public class CreateForumCommandValidator:AbstractValidator<CreateForumCommand>
{
    public CreateForumCommandValidator()
    {
        RuleFor(f => f.Title)
            .NotEmpty().WithErrorCode("Empty")
            .MaximumLength(100).WithErrorCode("Length title too long");
    }
}