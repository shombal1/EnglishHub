using EnglishHub.Forums.Domain.Authentication;
using EnglishHub.Forums.Domain.UseCases.CreateForum;
using FluentAssertions;
using Moq;

namespace EnglishHub.Forums.Domain.Tests.Authorization;

public class ForumIntentionResolveShould
{
    private readonly ForumIntentionResolve _sut = new ForumIntentionResolve();


    [Fact]
    public void ReturnFalse_WhenEnumNotExists()
    {
        ForumIntention intention = (ForumIntention)(-1);

        var actual =  _sut.IsAllowed(new Mock<IIdentity>().Object, intention);

        actual.Should().BeFalse();
    }

    [Fact]
    public void ReturnFalse_WhenCheckingIntentionCreate_AndUserIsGuest()
    {
        var actual =  _sut.IsAllowed(User.Guest, ForumIntention.Create);

        actual.Should().BeFalse();
    }
    
    [Fact]
    public void ReturnTrue_WhenCheckingIntentionCreate_AndUserIsAuthentication()
    {
        var actual =  _sut.IsAllowed(new User(Guid.Parse("3F7EA8A8-74E2-463B-BA15-06E931906F3E"),Guid.Empty), ForumIntention.Create);

        actual.Should().BeTrue();
    }
}