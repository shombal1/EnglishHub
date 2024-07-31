using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.Authorization;
using EnglishHub.Domain.UseCases.SignOut;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Moq;
using Moq.Language.Flow;

namespace EnglishHub.Domain.Tests.SignOut;

public class SignOutUseCaseShould
{
    private readonly SignOutUseCase _sut;

    private readonly Mock<ISignOutStorage> _storage;

    private readonly ISetup<ISignOutStorage, Task> _removeSessionSetup;
    private readonly ISetup<IIdentityProvider, IIdentity> _currentIdentitySetup;
    private readonly ISetup<IIntentionManager, bool> _isAllowedSetup;

    public SignOutUseCaseShould()
    {
        _storage = new Mock<ISignOutStorage>();
        Mock<IIdentityProvider> identityProvider = new Mock<IIdentityProvider>();
        Mock<IIntentionManager> intentionManager = new Mock<IIntentionManager>();
        
        _removeSessionSetup = _storage.Setup(s =>
            s.RemoveSession(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));
        _currentIdentitySetup = identityProvider.Setup(i =>
            i.Current);
        _isAllowedSetup = intentionManager.Setup(i=>
            i.IsAllowed(It.IsAny<AccountIntention>()));

        _sut = new SignOutUseCase(_storage.Object, identityProvider.Object,intentionManager.Object);
    }

    [Fact]
    public async Task ThrowIntentionManagerException_WhenUserIsNotAuthenticated()
    {
        _currentIdentitySetup.Returns(User.Guest);
        _isAllowedSetup.Returns(false);
        
        await _sut.Invoking(s =>
            s.Handle(new SignOutCommand(),CancellationToken.None)).Should().ThrowAsync<IntentionManagerException>();
    }

    [Fact]
    public async Task RemoveCurrentIdentitySession()
    {
        Guid sessionId = Guid.Parse("1563A129-3595-4163-A299-0C442C85EB1B");
        _isAllowedSetup.Returns(true);
        _currentIdentitySetup.Returns(new User(Guid.Empty, sessionId));
        _removeSessionSetup.Returns(Task.CompletedTask);

        await _sut.Handle(new SignOutCommand(),CancellationToken.None);

        _storage.Verify(s =>
            s.RemoveSession(sessionId, It.IsAny<CancellationToken>()), Times.Once);
    }
}