using EnglishHub.Forums.Domain.Authentication;

namespace EnglishHub.Forums.Domain.Authorization;

public interface IIntentionResolve
{
    
}

public interface IIntentionResolve<in TIntention> : IIntentionResolve
{
    bool IsAllowed(IIdentity subject, TIntention intention);
}