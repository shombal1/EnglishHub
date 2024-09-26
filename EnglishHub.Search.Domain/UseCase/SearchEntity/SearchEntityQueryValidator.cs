using FluentValidation;

namespace EnglishHub.Search.Domain.UseCase.SearchEntity;

public class SearchEntityQueryValidator : AbstractValidator<SearchEntityQuery>
{
    public SearchEntityQueryValidator()
    {
        RuleFor(s => s.Query)
            .NotEmpty().WithErrorCode("Empty")
            .MaximumLength(200).WithErrorCode("too long");
    }
}