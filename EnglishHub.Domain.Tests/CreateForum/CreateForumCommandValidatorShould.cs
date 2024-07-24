using EnglishHub.Domain.UseCases.CreateForum;
using FluentAssertions;

namespace EnglishHub.Domain.Tests.CreateForum;

public class CreateForumCommandValidatorShould
{
    private readonly CreateForumCommandValidator _sut = new CreateForumCommandValidator();


    public static IEnumerable<object[]> ReturnInvalidCommands()
    {
        yield return new object[] { new CreateForumCommand("") };
        yield return new object[] { new CreateForumCommand("    ") };
        yield return new object[] { new CreateForumCommand(new string('a', 200)) };
    }

    [Theory]
    [MemberData(nameof(ReturnInvalidCommands))]
    public async Task ReturnFailure_WhenCommandIsValid(CreateForumCommand command)
    {
        var actual = await _sut.ValidateAsync(command);

        actual.IsValid.Should().BeFalse();
    }
}