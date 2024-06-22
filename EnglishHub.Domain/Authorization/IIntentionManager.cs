using EnglishHub.Domain.Authentication;

namespace EnglishHub.Domain.Authorization;

public interface IIntentionManager
{
    bool IsAllowed<TIntention>(TIntention intention) where TIntention : Enum;

    bool IsAllowed<TIntention, TObject>(TIntention intention, TObject target) where TIntention : Enum;
}

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

    public bool IsAllowed<TIntention, TObject>(TIntention intention, TObject target) where TIntention : Enum
    {
        throw new NotImplementedException();
    }
}

public static class IntentionManagerExtensions
{
    public static void
        ThrowIfForbidden<TIntention>(this IIntentionManager intentionManageк, TIntention intention)
        where TIntention : Enum

    {
        if (!intentionManageк.IsAllowed(intention))
        {
            throw new IntentionManagerException();
        }
    }
}