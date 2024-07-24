using EnglishHub.Domain.Authentication;

namespace EnglishHub.Domain.Authorization;

public class IntentionManager : IIntentionManager
{
    private readonly IEnumerable<IIntentionResolve> _resolvers;
    private readonly IIdentityProvider _identityProvider;

    public IntentionManager(IEnumerable<IIntentionResolve> resolvers, IIdentityProvider identityProvider)
    {
        _resolvers = resolvers;
        _identityProvider = identityProvider;
    }

    public bool IsAllowed<TIntention>(TIntention intention) where TIntention : Enum
    {
        var matchingResolver = _resolvers.OfType<IIntentionResolve<TIntention>>().FirstOrDefault();

        return matchingResolver?.IsAllowed(_identityProvider.Current, intention) ?? false;
    }
}