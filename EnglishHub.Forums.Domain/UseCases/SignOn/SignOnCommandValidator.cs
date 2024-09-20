using FluentValidation;

namespace EnglishHub.Forums.Domain.UseCases.SignOn;

public class SignOnCommandValidator : AbstractValidator<SignOnCommand>
{
    public SignOnCommandValidator()
    {
        RuleFor(s => s.Login)
            .NotEmpty().WithErrorCode("Empty login")
            .MaximumLength(30).WithErrorCode("login too long");

        RuleFor(s => s.Password)
            .NotEmpty().WithErrorCode("Empty password")
            .MaximumLength(50).WithErrorCode("password too long");
    }
}