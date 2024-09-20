using EnglishHub.Forums.Domain.UseCases.CreateTopic;
using FluentAssertions;

namespace EnglishHub.Forums.Domain.Tests.CreateTopic;

public class CreateTopicCommandValidatorShould
{
    private readonly CreateTopicCommandValidator _sut = new CreateTopicCommandValidator();

    public static IEnumerable<object[]> GetInvalidCommand()
    {
        var command = new CreateTopicCommand(
            Guid.Parse("3186D474-4CBD-44F9-A16D-39749B0AB0D7"), "Test");

        yield return [command with { ForumId = Guid.Empty }];
        yield return [command with { Title = "" }];
        yield return [command with { Title = "    " }];
        yield return [command with { Title = new string('a', 201) }];
    }

    [Theory]
    [MemberData(nameof(GetInvalidCommand))]
    public void ReturnFailure_WhenCommandIsValid(CreateTopicCommand command)
    {
        var actual = _sut.Validate(command);

        actual.IsValid.Should().BeFalse();
    }

    [Fact]
    public void ReturnSuccess_WhenCommandIsValid()
    {
        var command = new CreateTopicCommand(
            Guid.Parse("3186D474-4CBD-44F9-A16D-39749B0AB0D7"), "Test");

        var actual = _sut.Validate(command);

        actual.IsValid.Should().BeTrue();
    }
}