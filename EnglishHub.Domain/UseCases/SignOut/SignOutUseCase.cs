using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.Authorization;

namespace EnglishHub.Domain.UseCases.SignOut;

public class SignOutUseCase : ISignOutUseCase
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
    
    public async Task Execute(CancellationToken cancellationToken)
    {
        _intentionManager.ThrowIfForbidden(AccountIntention.SignOut);

        Guid sessionId = _identityProvider.Current.SessionId;
        await _storage.RemoveSession(sessionId, cancellationToken);

    }
}