using FluentValidation;

namespace EnglishHub.Domain.UseCases.SignInUseCase;

public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(s => s.Login)
            .NotEmpty().WithErrorCode("Empty login");

        RuleFor(s => s.Password)
            .NotEmpty().WithErrorCode("Empty password");
    }
}