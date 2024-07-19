using FluentValidation;

namespace EnglishHub.Domain.UseCases.SignOnUseCase;

public class SignOnCommandValidator : AbstractValidator<SignOnCommand>
{
    public SignOnCommandValidator()
    {
        RuleFor(s => s.Login)
            .NotEmpty().WithErrorCode("Empty login")
            .MaximumLength(30).WithErrorCode("login too long");

        RuleFor(s => s.Password)
            .NotEmpty().WithErrorCode("Empty password");
    }
}