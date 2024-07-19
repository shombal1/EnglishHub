using FluentValidation;

namespace EnglishHub.Domain.UseCases.CreateTopicUseCase;

public class CreateTopicCommandValidator : AbstractValidator<CreateTopicCommand>
{
    public CreateTopicCommandValidator()
    {
        RuleFor(a => a.ForumId)
            .NotEmpty().WithErrorCode("Empty");

        RuleFor(a => a.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithErrorCode("Empty")
            .MaximumLength(100).WithErrorCode("Length title too long");
    }
}