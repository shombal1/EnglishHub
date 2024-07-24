using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.UseCases.CreateTopic;
using FluentAssertions;
using Moq;

namespace EnglishHub.Domain.Tests.Authorization;

public class TopicIntentionResolveShould
{
    private readonly TopicIntentionResolve _sut = new TopicIntentionResolve();


    [Fact]
    public void ReturnFalse_WhenEnumNotExists()
    {
        TopicIntention intention = (TopicIntention)(-1);

       var actual =  _sut.IsAllowed(new Mock<IIdentity>().Object, intention);

       actual.Should().BeFalse();
    }

    [Fact]
    public void ReturnFalse_WhenCheckingIntentionCreate_AndUserIsGuest()
    {
        var actual =  _sut.IsAllowed(User.Guest, TopicIntention.Create);

        actual.Should().BeFalse();
    }
    
    [Fact]
    public void ReturnTrue_WhenCheckingIntentionCreate_AndUserIsAuthentication()
    {
        var actual =  _sut.IsAllowed(new User(Guid.Parse("3F7EA8A8-74E2-463B-BA15-06E931906F3E"),Guid.Empty), TopicIntention.Create);

        actual.Should().BeTrue();
    }
}