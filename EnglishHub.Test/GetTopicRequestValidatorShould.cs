using EnglishHub.Domain.UseCases.GetTopicUseCase;
using FluentAssertions;

namespace EnglishHub.Test;

public class GetTopicRequestValidatorShould
{
    private readonly GetTopicRequestValidator _sut = new GetTopicRequestValidator();

    public static IEnumerable<object[]> GetInvalidRequest()
    {
        var valid = new GetTopicRequest(Guid.Parse("CF8A8459-1064-4FA4-963B-B618BF84FEB2"), 0, 1);

        yield return new object[] { valid with { ForumId = Guid.Empty } };
        yield return new object[] { valid with { Skip = -12} };
        yield return new object[] { valid with { Take = -3} };
    }

    [Theory]
    [MemberData(nameof(GetInvalidRequest))]
    public void ReturnFailure_WhenRequestIsValid(GetTopicRequest request)
    {
        var actual = _sut.Validate(request);

        actual.IsValid.Should().BeFalse();
    }
}