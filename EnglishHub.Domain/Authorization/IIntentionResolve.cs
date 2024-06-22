using EnglishHub.Domain.Authentication;

namespace EnglishHub.Domain.Authorization;

public interface IIntentionResolve
{
    
}

public interface IIntentionResolve<in TIntention> : IIntentionResolve
{
    bool IsAllowed(IIdentity subject, TIntention intention);
}