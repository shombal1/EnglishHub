using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.Authorization;
using EnglishHub.Domain.Models;
using EnglishHub.Domain.UseCases.CreateForum;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Moq.Language.Flow;

namespace EnglishHub.Domain.Tests.CreateForum;

public class CreateForumUseCaseShould
{
    public readonly CreateForumUseCase _sut;
    private readonly ISetup<IIntentionManager, bool> _intentionIsAllowedSetup;
    private readonly ISetup<ICreateForumStorage, Task<Forum>> _storageSetup;

    public CreateForumUseCaseShould()
    {
        var storage = new Mock<ICreateForumStorage>();
        var validator = new Mock<IValidator<CreateForumCommand>>();
        var intentionManager = new Mock<IIntentionManager>();
        // var identity = new Mock<IIdentity>();
        var identityProvider = new Mock<IIdentityProvider>();

        _intentionIsAllowedSetup = intentionManager.Setup(a => a.IsAllowed(It.IsAny<ForumIntention>()));
        _storageSetup = storage.Setup(s => s.CreateForum(It.IsAny<string>(),It.IsAny<CancellationToken>()));
        
        validator.Setup(v => v.ValidateAsync(It.IsAny<CreateForumCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        
        //identityProvider.Setup(e => e.Current).Returns(identity.Object);


        _sut = new CreateForumUseCase(storage.Object, validator.Object, intentionManager.Object,
            identityProvider.Object);
    }

    [Fact]
    public async Task ThrowIntentionManagerException_WhenNotAllowed()
    {
        CreateForumCommand command = new CreateForumCommand("wd");

        _intentionIsAllowedSetup.Returns(false);

        await _sut.Invoking(s => s.Execute(command,CancellationToken.None)).Should().ThrowAsync<IntentionManagerException>();
    }

    [Fact]
    public async Task ReturnCreatedForum()
    {
        CreateForumCommand command = new CreateForumCommand("some title");
        var expectedForum = new Forum()
        {
            Id = Guid.Parse("45B10205-C554-4080-96C8-B33BC17B82B5"),
            Title = "Title"
        };
        
        _intentionIsAllowedSetup.Returns(true);
        _storageSetup.ReturnsAsync(expectedForum);
        
        var createdForum = await _sut.Execute(command, CancellationToken.None);

        createdForum.Should().NotBeNull();
        createdForum.Should().BeSameAs(expectedForum);
    }
}