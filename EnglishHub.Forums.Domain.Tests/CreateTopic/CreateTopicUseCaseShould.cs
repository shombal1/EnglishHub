using EnglishHub.Forums.Domain.Authentication;
using EnglishHub.Forums.Domain.Authorization;
using EnglishHub.Forums.Domain.Exceptions;
using EnglishHub.Forums.Domain.Models;
using EnglishHub.Forums.Domain.UseCases.CreateTopic;
using EnglishHub.Forums.Domain.UseCases.GetForum;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Moq.Language.Flow;

namespace EnglishHub.Forums.Domain.Tests.CreateTopic;

public class CreateTopicUseCaseShould
{
    private readonly CreateTopicUseCase _sut;

    private readonly ISetup<ICreateTopicStorage, Task<Topic>> _createTopicStorageSetup;
    private readonly ISetup<IIdentity, Guid> _getCurrentUserIdSetup;
    private readonly ISetup<IIntentionManager, bool> _intentionIsAllowedSetup;
    private readonly ISetup<IGetForumStorage, Task<IEnumerable<Forum>>> _getForumStorageSetup;

    public CreateTopicUseCaseShould()
    {
        var storage = new Mock<ICreateTopicStorage>();
        var getForumsStorage = new Mock<IGetForumStorage>();
        var intentionManager = new Mock<IIntentionManager>();
        var identity = new Mock<IIdentity>();
        var identityProvider = new Mock<IIdentityProvider>();
        var validator = new Mock<IValidator<CreateTopicCommand>>();

        _createTopicStorageSetup = storage.Setup(s =>
            s.CreateTopic(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));
        _getForumStorageSetup = getForumsStorage.Setup(s => s.GetForums(It.IsAny<CancellationToken>()));
        identityProvider.Setup(p => p.Current).Returns(identity.Object);
        _getCurrentUserIdSetup = identity.Setup(s => s.UserId);
        _intentionIsAllowedSetup = intentionManager.Setup(m => m.IsAllowed(It.IsAny<TopicIntention>()));

        validator.Setup(v =>
                v.ValidateAsync(It.IsAny<CreateTopicCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _sut = new(identityProvider.Object, getForumsStorage.Object,
            intentionManager.Object, validator.Object, null);
    }

    [Fact]
    public async Task ThrowIntentionManagerException_WhenTopicCreatedNotAllowed()
    {
        var forumId = Guid.Parse("5E1DCF96-E8F3-41C9-BD59-6479140933B3");
        _getForumStorageSetup.ReturnsAsync([new Forum() { Id = forumId }]);

        _intentionIsAllowedSetup.Returns(false);

        await _sut.Invoking(t => t.Handle(new CreateTopicCommand(forumId, "forum"), CancellationToken.None)).Should()
            .ThrowAsync<IntentionManagerException>();
    }

    [Fact]
    public async Task ThrowForumNotFoundException_WhenNoMatchingForum()
    {
        var forumId = Guid.Parse("5E1DCF96-E8F3-41C9-BD59-6479140933B3");

        _intentionIsAllowedSetup.Returns(true);
        _getForumStorageSetup.ReturnsAsync(Array.Empty<Forum>());

        await _sut.Invoking(s => s.Handle(new CreateTopicCommand(forumId, "Some title"), CancellationToken.None))
            .Should().ThrowAsync<ForumNotFoundException>();
    }
}