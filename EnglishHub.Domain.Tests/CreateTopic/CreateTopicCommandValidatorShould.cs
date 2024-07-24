using EnglishHub.Domain.UseCases.CreateTopic;
using FluentAssertions;

namespace EnglishHub.Domain.Tests.CreateTopic;

public class CreateTopicCommandValidatorShould
{
    private readonly CreateTopicCommandValidator _sut = new CreateTopicCommandValidator();

    public static IEnumerable<object[]> GetInvalidCommand()
    {
        var command = new CreateTopicCommand(
            Guid.Parse("3186D474-4CBD-44F9-A16D-39749B0AB0D7"), "Test");

        yield return new object[] { command with { ForumId = Guid.Empty } };
        yield return new object[] { command with { Title = "" } };
        yield return new object[] { command with { Title = "    " } };
        yield return new object[] { command with { Title = new string('a',201) } };
    }

    [Theory]
    [MemberData(nameof(GetInvalidCommand))]
    public void ReturnFailure_WhenCommandIsValid(CreateTopicCommand command)
    {
        var actual =  _sut.Validate(command);

        actual.IsValid.Should().BeFalse();
    }
}