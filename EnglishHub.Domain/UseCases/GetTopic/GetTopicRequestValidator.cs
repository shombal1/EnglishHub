using FluentValidation;

namespace EnglishHub.Domain.UseCases.GetTopic;

public class GetTopicRequestValidator : AbstractValidator<GetTopicRequest>
{
    public GetTopicRequestValidator()
    {
        RuleFor(a => a.ForumId)
            .NotEmpty().WithErrorCode("Empty");
        
        RuleFor(a => a.Skip)
            .GreaterThanOrEqualTo(0).WithErrorCode("Invalid");
        
        RuleFor(a => a.Take)
            .GreaterThanOrEqualTo(0).WithErrorCode("Invalid");
    }
}