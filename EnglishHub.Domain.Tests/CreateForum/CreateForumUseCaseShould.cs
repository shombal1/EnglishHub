using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.Authorization;
using EnglishHub.Domain.Models;
using EnglishHub.Domain.UseCases.CreateForumUseCase;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Moq.Language.Flow;

namespace EnglishHub.Domain.Tests.CreateForum;

public class CreateForumUseCaseShould
{
    public readonly CreateForumUseCase sut;
    private readonly ISetup<IIntentionManager, bool> _intentionIsAllowedSetup;
    
    public CreateForumUseCaseShould()
    {
        var storage = new Mock<ICreateForumStorage>();
        var validator = new Mock<IValidator<CreateForumCommand>>();
        var intentionManager = new Mock<IIntentionManager>();
        // var identity = new Mock<IIdentity>();
        var identityProvider = new Mock<IIdentityProvider>();

        _intentionIsAllowedSetup = intentionManager.Setup(a => a.IsAllowed(It.IsAny<ForumIntention>()));
        validator.Setup(v => v.ValidateAsync(It.IsAny<CreateForumCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        
        //identityProvider.Setup(e => e.Current).Returns(identity.Object);


        sut = new CreateForumUseCase(storage.Object, validator.Object, intentionManager.Object,
            identityProvider.Object);
    }

    [Fact]
    public async Task ThrowIntentionManagerException_WhenNotAllowed()
    {
        CreateForumCommand command = new CreateForumCommand("wd");

        _intentionIsAllowedSetup.Returns(false);

        await sut.Invoking(s => s.Execute(command,CancellationToken.None)).Should().ThrowAsync<IntentionManagerException>();
    }
}