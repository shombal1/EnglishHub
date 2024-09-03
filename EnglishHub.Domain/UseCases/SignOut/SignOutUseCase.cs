using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.Authorization;
using MediatR;

namespace EnglishHub.Domain.UseCases.SignOut;

public class SignOutUseCase(
    ISignOutStorage storage,
    IIdentityProvider identityProvider,
    IIntentionManager intentionManager)
    : IRequestHandler<SignOutCommand>
{
    public Task Handle(SignOutCommand command,CancellationToken cancellationToken)
    {
        intentionManager.ThrowIfForbidden(AccountIntention.SignOut);

        Guid sessionId = identityProvider.Current.SessionId;
        
        return storage.RemoveSession(sessionId, cancellationToken);
    }
}