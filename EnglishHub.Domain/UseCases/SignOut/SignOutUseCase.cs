using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.Authorization;
using MediatR;

namespace EnglishHub.Domain.UseCases.SignOut;

public class SignOutUseCase : IRequestHandler<SignOutCommand>
{
    private readonly ISignOutStorage _storage;
    private readonly IIdentityProvider _identityProvider;
    private readonly IIntentionManager _intentionManager;

    public SignOutUseCase(
        ISignOutStorage storage,
        IIdentityProvider identityProvider,
        IIntentionManager intentionManager)
    {
        _storage = storage;
        _identityProvider = identityProvider;
        _intentionManager = intentionManager;
    }
    
    public Task Handle(SignOutCommand command,CancellationToken cancellationToken)
    {
        _intentionManager.ThrowIfForbidden(AccountIntention.SignOut);

        Guid sessionId = _identityProvider.Current.SessionId;
        
        return _storage.RemoveSession(sessionId, cancellationToken);
    }
}