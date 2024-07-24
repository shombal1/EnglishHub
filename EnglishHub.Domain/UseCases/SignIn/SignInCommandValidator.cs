using FluentValidation;

namespace EnglishHub.Domain.UseCases.SignIn;

public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(s => s.Login)
            .NotEmpty().WithErrorCode("Empty login")
            .MaximumLength(30).WithErrorCode("Login too long");

        RuleFor(s => s.Password)
            .NotEmpty().WithErrorCode("Empty password")
            .MaximumLength(50).WithErrorCode("Password too long");
    }
}