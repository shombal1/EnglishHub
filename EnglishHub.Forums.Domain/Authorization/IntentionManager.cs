using EnglishHub.Forums.Domain.Authentication;

namespace EnglishHub.Forums.Domain.Authorization;

public class IntentionManager(
    IEnumerable<IIntentionResolve> resolvers, 
    IIdentityProvider identityProvider)
    : IIntentionManager
{
    public bool IsAllowed<TIntention>(TIntention intention) where TIntention : Enum
    {
        var matchingResolver = resolvers.OfType<IIntentionResolve<TIntention>>().FirstOrDefault();

        return matchingResolver?.IsAllowed(identityProvider.Current, intention) ?? false;
    }
}