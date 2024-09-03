using EnglishHub.Domain.UseCases.GetTopic;
using FluentAssertions;

namespace EnglishHub.Domain.Tests.GetTopic;

public class GetTopicRequestValidatorShould
{
    private readonly GetTopicQueryValidator _sut = new GetTopicQueryValidator();

    public static IEnumerable<object[]> GetInvalidRequest()
    {
        var valid = new GetTopicQuery(Guid.Parse("CF8A8459-1064-4FA4-963B-B618BF84FEB2"), 0, 1);

        yield return [valid with { ForumId = Guid.Empty }];
        yield return [valid with { Skip = -12}];
        yield return [valid with { Take = -3}];
    }

    [Theory]
    [MemberData(nameof(GetInvalidRequest))]
    public void ReturnFailure_WhenRequestIsValid(GetTopicQuery query)
    {
        var actual = _sut.Validate(query);

        actual.IsValid.Should().BeFalse();
    }
}