using EnglishHub.Forums.Domain.UseCases.SignOn;
using FluentAssertions;

namespace EnglishHub.Forums.Domain.Tests.SignOn;

public class SignOnCommandValidatorShould
{
    private readonly SignOnCommandValidator _sut=new SignOnCommandValidator();

    public static IEnumerable<object[]> ReturnInvalidCommand()
    {
        SignOnCommand validCommand = new SignOnCommand("best login", "qwerty");
    
        yield return [validCommand with { Login = "" }];
        yield return [validCommand with { Login = "   " }];
        yield return [validCommand with { Login = new string('a',31) }];
        yield return [validCommand with { Password = "" }];
        yield return [validCommand with { Password = "   " }];
        yield return [validCommand with { Password = new string('a',51) }];
    }
    
    [Theory]
    [MemberData(nameof(ReturnInvalidCommand))]
    public async Task ReturnFailure_WhenCommandIsValid(SignOnCommand command)
    {
        var actual = await _sut.ValidateAsync(command);
    
        actual.IsValid.Should().BeFalse();
    }
}