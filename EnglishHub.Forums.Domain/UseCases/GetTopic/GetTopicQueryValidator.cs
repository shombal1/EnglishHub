using FluentValidation;

namespace EnglishHub.Forums.Domain.UseCases.GetTopic;

public class GetTopicQueryValidator : AbstractValidator<GetTopicQuery>
{
    public GetTopicQueryValidator()
    {
        RuleFor(a => a.ForumId)
            .NotEmpty().WithErrorCode("Empty");
        
        RuleFor(a => a.Skip)
            .GreaterThanOrEqualTo(0).WithErrorCode("Invalid");
        
        RuleFor(a => a.Take)
            .GreaterThanOrEqualTo(0).WithErrorCode("Invalid");
    }
}