using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.UseCases.SignOut;
using FluentAssertions;
using Moq;

namespace EnglishHub.Domain.Tests.Authorization;

public class AccountIntentionResolveShould
{
    private readonly AccountIntentionResolve _sut = new AccountIntentionResolve();
    
    [Fact]
    public void ReturnFalse_WhenEnumNotExists()
    {
        AccountIntention intention = (AccountIntention)(-1);

        var actual =  _sut.IsAllowed(new Mock<IIdentity>().Object, intention);

        actual.Should().BeFalse();
    }

    [Fact]
    public void ReturnFalse_WhenCheckingIntentionSignOut_AndUserIsGuest()
    {
        var actual =  _sut.IsAllowed(User.Guest, AccountIntention.SignOut);

        actual.Should().BeFalse();
    }
    
    [Fact]
    public void ReturnTrue_WhenCheckingIntentionSignOut_AndUserIsAuthentication()
    {
        var actual =  _sut.IsAllowed(new User(Guid.Parse("3F7EA8A8-74E2-463B-BA15-06E931906F3E"),Guid.Empty), AccountIntention.SignOut);

        actual.Should().BeTrue();
    }
}