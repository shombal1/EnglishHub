using EnglishHub.Domain.UseCases.SignOn;
using FluentAssertions;

namespace EnglishHub.Domain.Tests.SignOn;

public class SignOnCommandValidatorShould
{
    private readonly SignOnCommandValidator _sut=new SignOnCommandValidator();

    public static IEnumerable<object[]> ReturnInvalidCommand()
    {
        SignOnCommand validCommand = new SignOnCommand("best login", "qwerty");

        yield return new object[] { validCommand with { Login = "" } };
        yield return new object[] { validCommand with { Login = "   " } };
        yield return new object[] { validCommand with { Login = new string('a',31) } };
        yield return new object[] { validCommand with { Password = "" } };
        yield return new object[] { validCommand with { Password = "   " } };
        yield return new object[] { validCommand with { Password = new string('a',51) } };
    }

    [Theory]
    [MemberData(nameof(ReturnInvalidCommand))]
    public async Task ReturnFailure_WhenCommandIsValid(SignOnCommand command)
    {
        var actual = await _sut.ValidateAsync(command);

        actual.IsValid.Should().BeFalse();
    }
}