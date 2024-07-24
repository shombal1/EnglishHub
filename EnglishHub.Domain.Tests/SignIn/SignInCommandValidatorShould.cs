using EnglishHub.Domain.UseCases.SignIn;
using FluentAssertions;

namespace EnglishHub.Domain.Tests.SignIn;

public class SignInCommandValidatorShould
{
    private readonly SignInCommandValidator _sut=new SignInCommandValidator();

    public static IEnumerable<object[]> ReturnInvalidCommand()
    {
        SignInCommand validCommand = new SignInCommand("best login", "qwerty");

        yield return new object[] { validCommand with { Login = "" } };
        yield return new object[] { validCommand with { Login = "   " } };
        yield return new object[] { validCommand with { Login = new string('a',31) } };
        yield return new object[] { validCommand with { Password = "" } };
        yield return new object[] { validCommand with { Password = "   " } };
        yield return new object[] { validCommand with { Password = new string('a',51) } };
    }

    [Theory]
    [MemberData(nameof(ReturnInvalidCommand))]
    public async Task ReturnFailure_WhenCommandIsValid(SignInCommand command)
    {
        var actual = await _sut.ValidateAsync(command);

        actual.IsValid.Should().BeFalse();
    }
}